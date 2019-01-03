using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAidBoost : MonoBehaviour {

    GameObject player;                          // Reference to the player GameObject.
    PlayerHealth playerHealth;                  // Reference to the player's health.


    void Awake()
    {
        // Setting up the references.
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    void OnTriggerEnter(Collider other)
    {
        // If the entering collider is the player...
        
        if (other.gameObject == player)
        {            
            playerHealth.currentHealth = playerHealth.startingHealth;
            playerHealth.healthSlider.value = playerHealth.startingHealth;
        }
    }
    
}
