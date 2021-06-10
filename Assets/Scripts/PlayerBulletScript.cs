using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{

     public AlienManager alienManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * 8f * Time.deltaTime );
    }


    private void OnCollisionEnter2D(Collision2D other) 
    {
        // Try to communicate with Alien Manager...
        alienManager.bulletHitAlien(other.gameObject);
        Destroy(this.gameObject);
    }

    private void OnBecameInvisible() 
    {
        Debug.Log("Bullet wasted!");
        Destroy(this.gameObject);
    }
}
