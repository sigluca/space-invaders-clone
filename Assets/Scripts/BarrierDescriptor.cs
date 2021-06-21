using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierDescriptor : MonoBehaviour
{
    // How many hit could sustain?
    private int health;
    void Start()
    {
        health = Globals.barrierMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void deteriorate()
    {
        // Remove pixels as deterioration effect
        Sprite barrierSprite = this.gameObject.GetComponent<SpriteRenderer>().sprite;

        Debug.Log("W: "+barrierSprite.texture.width+" H:"+barrierSprite.texture.height);
        Debug.Log("Rect: "+barrierSprite.rect);
        Debug.Log("TextureRect: "+barrierSprite.textureRect);

        //for(int i=0;i<barrierSprite.texture.height;i++)
        //    for(int j=0;j<barrierSprite.texture.width;j++)

        //         barrierSprite.texture.SetPixel(j,i,new Color(0,1,0,1));
       
        //barrierSprite.texture.Apply();

    }


    
}
