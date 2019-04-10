using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAidBoost : MonoBehaviour {

      void OnTriggerEnter(Collider collision)
    {
        // If the entering collider is the player...
        GameObject hit = collision.gameObject;
        PlayerHealth playerHealth = hit.GetComponent<PlayerHealth>();
		
        if (playerHealth != null)
        {            
            playerHealth.currentHealth = playerHealth.startingHealth;
            playerHealth.healthSlider.value = playerHealth.startingHealth;
        }
    }
    
}
