using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShipMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public int shipSpeed  = 100;
    public GameObject bulletPrefab;
    public AlienManager alienManager;

    private GameObject bullet = null;

    void Start()
    {
        Debug.Log("Horde direction: "+Globals.hordeDirection);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector2.left * shipSpeed * Time.deltaTime );
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector2.right * shipSpeed * Time.deltaTime );
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Fire!");
            if(bullet==null)
            {
                Vector3 bulletStartPoint = new Vector3(transform.position.x,transform.position.y+0.5f,0);

                bullet = Instantiate(bulletPrefab,bulletStartPoint,Quaternion.identity);

                // We need to pass the Alien Manager (object) to the new spawned bullet, so it can
                // 'talk' with that to say a collision was detected...
                bullet.GetComponent<PlayerBulletScript>().alienManager = this.alienManager;
            }
        }

    }
}
