using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierDescriptor : MonoBehaviour
{
    // How many hit could sustain?
    private int health;
    private float hitDamagePercentage;
    void Start()
    {
        // How many hit can sustain the barrier before being destroyed
        health = Globals.barrierMaxHealth;

        // How many pixel (in percentage) to remove from barrier
        hitDamagePercentage = (1.0f / health) ; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void deteriorate()
    {
        // Reduce health...
        health--;

        // Check if destroyed...

        if(health<=0) 
        {
            Destroy(this.gameObject);
        }
        else
        {
            // Remove pixels as deterioration effect
            Sprite barrierSprite = this.gameObject.GetComponent<SpriteRenderer>().sprite;

            // Logging...
            Debug.Log("W: "+barrierSprite.texture.width+" H:"+barrierSprite.texture.height);
            Debug.Log("Rect: "+barrierSprite.rect);
            Debug.Log("TextureRect: "+barrierSprite.textureRect);
            Debug.Log("Sprite:"+barrierSprite);

            // Create a new texture
            Texture2D deterioratedTexture = new Texture2D((int) barrierSprite.rect.width,(int) barrierSprite.rect.height,TextureFormat.RGB24,false);

            // Copy old texture into the new one

            Graphics.CopyTexture(barrierSprite.texture,0,0,
            (int) barrierSprite.textureRect.x,(int) barrierSprite.textureRect.y,
            (int) barrierSprite.textureRect.width,(int) barrierSprite.textureRect.height,
            deterioratedTexture,0,0,0,0);

            // Il pivot dello sprite originale è nel centro (new Vector2(0.5f,0.5f))
            
            Sprite newSprite = Sprite.Create(deterioratedTexture, 
            new Rect(0, 0, barrierSprite.rect.width, barrierSprite.rect.height), 
            new Vector2(0.5f,0.5f),barrierSprite.pixelsPerUnit);
            this.gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;

            

            int totalPixel = (int)(barrierSprite.rect.width * barrierSprite.rect.height);
            int howManyPixelToRemove = (int)(totalPixel * hitDamagePercentage );
            
            Debug.Log("totalPixel:"+totalPixel);
            Debug.Log("hitDamagePercentage:"+hitDamagePercentage);
            Debug.Log("howManyPixelToRemove:"+howManyPixelToRemove);
            for(int i=0;i<howManyPixelToRemove;i++)
            {
                int y = Random.Range(0,deterioratedTexture.height);
                int x = Random.Range(0,deterioratedTexture.width);
                
                Debug.Log("Color:"+deterioratedTexture.GetPixel(x,y));
                if(deterioratedTexture.GetPixel(x,y) == Color.white)
                {
                    deterioratedTexture.SetPixel(x, y, Color.black);
                }
                
            }

            // Bonus to compensate missed pixel remove
            hitDamagePercentage+=0.2f;

            deterioratedTexture.Apply();
        }

    }
    


    
}
