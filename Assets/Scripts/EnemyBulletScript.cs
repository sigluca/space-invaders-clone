using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{

     public AlienManager alienManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * 2f * Time.deltaTime );
    }


    private void OnCollisionEnter2D(Collision2D other) 
    {
        // Various cases:

        

        if(other.gameObject.tag == "Barrier")
        {
            // 1: Barrier
            Debug.Log("Hit a barrier!");

            // Tell the barrier to deteriorate itself

            other.gameObject.GetComponent<BarrierDescriptor>().deteriorate();
            Destroy(this.gameObject);
        }
        else
        {
            // 2: Alien
            // Try to communicate the evento to the Alien Manager

            Debug.Log("Hit an alien!");
            //alienManager.bulletHitAlien(other.gameObject);
        }

       

    }

    private void OnBecameInvisible() 
    {
        Debug.Log("Bullet wasted!");
        Destroy(this.gameObject);
    }
}
