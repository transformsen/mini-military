using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    Transform player;               // Reference to the player's position.
    PlayerHealth playerHealth;      // Reference to the player's health.
    EnemyHealth enemyHealth;        // Reference to this enemy's health.
    UnityEngine.AI.NavMeshAgent nav;               // Reference to the nav mesh agent.
    
    void Awake()
    {
        // Set up the references.
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        //playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }


    void Update()
    {
        GameObject closePlayerObject = findClosetPlayer();
		if(closePlayerObject != null){
			player = findClosetPlayer().transform;
		}
		
		
		if(player != null){
			playerHealth = player.GetComponent<PlayerHealth>();
			
			// If the enemy and the player have health left...		
			if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
			{	
				// ... set the destination of the nav mesh agent to the player.
				if(player != null){
					nav.SetDestination(player.position);
				}else{
					//nav.enabled = false;
					nav.SetDestination(new Vector3(0,0,0));
				}
			}
			// Otherwise...
			else
			{
				// ... disable the nav mesh agent.
				//nav.enabled = false;
				nav.SetDestination(new Vector3(0,0,0));
			}
		}else{
			//nav.enabled = false;
			nav.SetDestination(new Vector3(0,0,0));
		}
    }
	
	GameObject findClosetPlayer(){		
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Player");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        // Iterate through them and find the closest one
		foreach (GameObject go in gos){  		
            Vector3 diff = (go.transform.position - position);
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance) {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;		
	}
}
