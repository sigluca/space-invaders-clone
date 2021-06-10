using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDescriptor : MonoBehaviour
{
    public int score;

    private AudioSource audioSource = null;
    private AudioClip alienDyingSound;

    // Start is called before the first frame update
    void Start()
    {
        
        alienDyingSound = (AudioClip) Resources.Load("Sounds/enemy die");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        // Switch to Alien Dying animation...

        Animator animator = this.gameObject.GetComponent<Animator>();
        animator.Play("Alien Dying", -1, 0);
        animator.Update(0f);

        // Create on-the-fly an AudioSource and play the 'Alien Dying Cry'
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = alienDyingSound;
        audioSource.Play();
        Destroy(this.gameObject,0.4f);
    }   
}
