using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftLimitScript : MonoBehaviour
{
    public AlienManager alienManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     private void OnTriggerEnter2D(Collider2D other) 
    {
        Debug.Log("Hit left Limit!"); 
        alienManager.hitLeftLimit();   
    }
}
