using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
    public int attackDamage = 10;               // The amount of health taken away per attack.


    Animator anim;                              // Reference to the animator component.
    GameObject player;                          // Reference to the player GameObject.
    PlayerHealth playerHealth;                  // Reference to the player's health.
    EnemyHealth enemyHealth;                    // Reference to this enemy's health.
    bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
    float timer;                                // Timer for counting up to the next attack.


    void Awake()
    {
        // Setting up the references.
        //player = GameObject.FindGameObjectWithTag("Player");
        //playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
    }


    void OnTriggerEnter(Collider collision)
    {
		GameObject hit = collision.gameObject;
        playerHealth = hit.GetComponent<PlayerHealth>();
		
        if (playerHealth != null)
        {
			 playerInRange = true;
			
		}
        
    }


    void OnTriggerExit(Collider collision)
    {
        GameObject hit = collision.gameObject;
        playerHealth = hit.GetComponent<PlayerHealth>();
		
        if (playerHealth != null)
        {
			 playerInRange = false;
			
		}
    }


    void Update()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
        if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            // ... attack.
            Attack();
        }
        else if(playerHealth != null && playerHealth.currentHealth > 0)
        {
            anim.SetTrigger("EnemyWalk");
        }

        // If the player has zero or less health...
        if (playerHealth != null && playerHealth.currentHealth <= 0)
        {
            // ... tell the animator the player is dead.
            //anim.SetTrigger("PlayerDead");
        }
    }


    void Attack()
    {
        // Reset the timer.
        timer = 0f;

        // If the player has health to lose...
        if (playerHealth != null && playerHealth.currentHealth > 0)
        {
            // ... damage the player.
            anim.SetTrigger("Attack");
            playerHealth.TakeDamage(attackDamage);            
        }
    }
}
