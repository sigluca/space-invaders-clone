using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlienManager : MonoBehaviour
{
    public float startingGameSpeed;

    
    private float game_speed;
    public GameObject scoreText;
    public GameObject highScoreText;
    public GameObject livesText;

    // List of Aliens
    private List<GameObject> aliens = new List<GameObject>();

    // Matrix of Aliens

    private GameObject[,] aliensMatrix = new GameObject[5,11];


    private List<GameObject> barriers = new List<GameObject>();

    private float last_tick_time;

    private AudioSource audioSource = null;
    private AudioClip[] alienWalkingSounds;
    
    private int currentAlienWalkingSound = 0;


    public GameObject enemyBulletPrefab;
    private GameObject enemyBullet = null;
    

    // Start is called before the first frame update
    void Start()
    {

        // Loading Alien Prefabs from Resource (avoid passing through IDE)

        GameObject alien1Prefab = (GameObject) Resources.Load("Prefabs/Alien 1");
        GameObject alien2Prefab = (GameObject) Resources.Load("Prefabs/Alien 2");
        GameObject alien3Prefab = (GameObject) Resources.Load("Prefabs/Alien 3");

        // Loading Barrier Prefabs from Resource (avoid passing through IDE)

        GameObject barrier11Prefab = (GameObject) Resources.Load("Prefabs/Barrier 1-1");
        GameObject barrier12Prefab = (GameObject) Resources.Load("Prefabs/Barrier 1-2");
        GameObject barrier13Prefab = (GameObject) Resources.Load("Prefabs/Barrier 1-3");
        GameObject barrier21Prefab = (GameObject) Resources.Load("Prefabs/Barrier 2-1");
        GameObject barrier22Prefab = (GameObject) Resources.Load("Prefabs/Barrier 2-2");
        GameObject barrier23Prefab = (GameObject) Resources.Load("Prefabs/Barrier 2-3");

         // Get the reference to audio source (to play audio clips)

        audioSource = this.GetComponent<AudioSource>();

        // Load alien audio clips directly from resources

        alienWalkingSounds = new AudioClip[4];
        alienWalkingSounds[0] = (AudioClip) Resources.Load("Sounds/fastinvader1");
        alienWalkingSounds[1] = (AudioClip) Resources.Load("Sounds/fastinvader2");
        alienWalkingSounds[2] = (AudioClip) Resources.Load("Sounds/fastinvader3");
        alienWalkingSounds[3] = (AudioClip) Resources.Load("Sounds/fastinvader4");
        currentAlienWalkingSound = 0;

        // Enemy rows initialization

        // First row - Alien 1
        for(int i=0;i<11;i++)
        {
            GameObject newbornAlien = Instantiate(alien1Prefab,new Vector3(-5+i,3,0),Quaternion.identity);
            aliens.Add(newbornAlien);
            aliensMatrix[0,i] = newbornAlien;
        }
        // Second & third rows - Alien 2
        for(int i=0;i<11;i++)
        {
            GameObject newbornAlien = Instantiate(alien2Prefab,new Vector3(-5+i,2,0),Quaternion.identity);
            aliens.Add(newbornAlien);
            aliensMatrix[1,i] = newbornAlien;
        }
        for(int i=0;i<11;i++)
        {
            GameObject newbornAlien = Instantiate(alien2Prefab,new Vector3(-5+i,1,0),Quaternion.identity);
            aliens.Add(newbornAlien);
            aliensMatrix[2,i] = newbornAlien;
        }
        // fourth & fifth rows - Alien 3
        for(int i=0;i<11;i++)
        {
            GameObject newbornAlien = Instantiate(alien3Prefab,new Vector3(-5+i,0,0),Quaternion.identity);
            aliens.Add(newbornAlien);
            aliensMatrix[3,i] = newbornAlien;
        }
        for(int i=0;i<11;i++)
        {
            GameObject newbornAlien = Instantiate(alien3Prefab,new Vector3(-5+i,-1,0),Quaternion.identity);
            aliens.Add(newbornAlien);
            aliensMatrix[4,i] = newbornAlien;
        }

        
        // Barrier initialization

        for(int i = 0; i < 5; i++)
        {
            float xOffset = i * 2.99f;
            barriers.Add(Instantiate(barrier11Prefab,new Vector3(-6.37f+xOffset,-3.38f,0),Quaternion.identity));
            barriers.Add(Instantiate(barrier12Prefab,new Vector3(-5.97f+xOffset,-3.38f,0),Quaternion.identity));
            barriers.Add(Instantiate(barrier13Prefab,new Vector3(-5.57f+xOffset,-3.38f,0),Quaternion.identity));
            barriers.Add(Instantiate(barrier21Prefab,new Vector3(-6.37f+xOffset,-3.78f,0),Quaternion.identity));
            barriers.Add(Instantiate(barrier22Prefab,new Vector3(-5.97f+xOffset,-3.78f,0),Quaternion.identity));
            barriers.Add(Instantiate(barrier23Prefab,new Vector3(-5.57f+xOffset,-3.78f,0),Quaternion.identity));
        }


        // Timing initialization

        last_tick_time = Time.time;
        game_speed = startingGameSpeed;

        // Score & lives initialization

        scoreText.GetComponent<Text>().text = "Score: "+Globals.score;
        highScoreText.GetComponent<Text>().text = "Score: "+Globals.highScore;
        livesText.GetComponent<Text>().text = "Score: "+Globals.lives;

    }

    // Update is called once per frame
    void Update()
    {
        float elapsedTime = Time.time - last_tick_time;
        //Debug.Log("elapsed:"+elapsedTime);
        if(elapsedTime > game_speed )
        {
            // For each aliens...
            foreach (var alien in aliens)
            {
                // Move each aliens in the global horde direction...

                alien.transform.position += new Vector3(Globals.hordeDirection*0.1f,0,0);
                
                // Get animator reference
                Animator animator = alien.GetComponent<Animator>();
                AnimatorClipInfo[] m_CurrentClipInfo = animator.GetCurrentAnimatorClipInfo(0);
                
                // Read the current animation time
                float normalizedTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
                // Debug.Log("time: " + normalizedTime );
                
                // Switch frames using time...
                if(normalizedTime >= 0.5f) normalizedTime = 0f;
                else normalizedTime = 0.5f;

                // Update frame of current animation
                animator.Play(m_CurrentClipInfo[0].clip.name, -1, normalizedTime);
                animator.Update(0f);

               
                
            }

            // Enemies could fires?
    
            if(enemyBullet==null)
            {
                // Find a suitable alien to fire (uncovered by others)

                GameObject frontLineAlien;

                // Generate a random column
                int col = Random.Range(0,10);
                
                // Find the 'front line' alien
                int row = 4;
                
                frontLineAlien = aliensMatrix[row,col];
                while(frontLineAlien==null && row > 0)
                {
                    row--;
                    frontLineAlien = aliensMatrix[row,col];
                }

                // If we find an alien... fire! 
                if(frontLineAlien!=null)
                {
                    Debug.Log("Enemy Fire at row: "+row+" col: "+col);

                    // Firing sound: alien bullets are mute?

                    //audioSource.clip = shootAudioClip;
                    //audioSource.Play();
                
                
                    Vector3 bulletStartPoint = new Vector3(frontLineAlien.transform.position.x,frontLineAlien.transform.position.y-0.25f,0);

                    enemyBullet = Instantiate(enemyBulletPrefab,bulletStartPoint,Quaternion.identity);

                    // We need to pass the Alien Manager (object) to the new spawned bullet, so it can
                    // 'talk' with that to say a collision was detected...


                    enemyBullet.GetComponent<EnemyBulletScript>().alienManager = this;
       

                }

                // If no alien was found, we skip this frame... maybe next frame
                // will be more lucky
                
            }
            

            // Play Enemies sounds
            audioSource.clip = alienWalkingSounds[currentAlienWalkingSound];
            audioSource.Play();
            currentAlienWalkingSound = (currentAlienWalkingSound+1)%4;

            last_tick_time = Time.time; 
        }
    }

    public void hitRightLimit()
    {
        if(Globals.hordeDirection==Globals.HORDE_GOING_RIGHT)
        {
            Globals.hordeDirection=Globals.HORDE_GOING_LEFT;
            foreach (var alien in aliens)
            {
                alien.transform.position += new Vector3(0,-0.25f,0);
            }
        }
    }

    public void hitLeftLimit()
    {
        if(Globals.hordeDirection==Globals.HORDE_GOING_LEFT)
        {
            Globals.hordeDirection=Globals.HORDE_GOING_RIGHT;
            foreach (var alien in aliens)
            {
                alien.transform.position += new Vector3(0,-0.25f,0);
            }
        }
    }

    public void bulletHitBullet(GameObject hittedBullet)
    {
        Destroy(hittedBullet);
        enemyBullet = null;
    }
    public void bulletHitAlien(GameObject hittedAlien)
    {
        Debug.Log("Hitted: "+hittedAlien.tag);

        // Remove hitted alien from alien List...
        switch(hittedAlien.tag) 
        {
            case "Alien 1":
            case "Alien 2":
            case "Alien 3":

                // Play alien moan...
                //audioSource.clip = alienDyingSound;
                //audioSource.Play();
                int enemyScore = hittedAlien.GetComponent<EnemyDescriptor>().score;
                Globals.score+=enemyScore;
                scoreText.GetComponent<Text>().text = "Score: "+Globals.score;


                aliens.Remove(hittedAlien);

                // Check position in original grid

                for(int col=0;col<11;col++)
                for(int row=0;row<5;row++)
                    if(aliensMatrix[row,col]==hittedAlien) 
                    {
                        // Killed aliens must be removed from the grid too
                        aliensMatrix[row,col]=null;
                    }

                

                hittedAlien.GetComponent<EnemyDescriptor>().Die();
                //Destroy(hittedAlien);

            break;    
        }
        
    }
}
