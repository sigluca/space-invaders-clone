using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightLimitScript : MonoBehaviour
{
    // Start is called before the first frame update
    public AlienManager alienManager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Debug.Log("Hit Right Limit!"); 
        alienManager.hitRightLimit();   
    }
}
