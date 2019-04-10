using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{

    public Slider healthSlider;                                 // Reference to the UI's health bar.
    public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
    public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.

     void Awake()
    {
        healthSlider = GetComponentInChildren<Slider>();
        damageImage = GetComponentInChildren<Image>();
        Debug.Log("healthSlider="+healthSlider);
        Debug.Log("damageImage="+damageImage);
    }

    public void ToggleFalshImage(bool damaged){
        // If the player has just been damaged...
        if (damaged)
        {
            // ... set the colour of the damageImage to the flash colour.
            damageImage.color = flashColour;
        }
        // Otherwise...
        else
        {
            // ... transition the colour back to clear.
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
    }

    public void updateHealthSlider(int currentHealth){
       // Set the health bar's value to the current health.
        healthSlider.value = currentHealth; 
    } 
}
