using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyManager : NetworkBehaviour {

    PlayerHealth playerHealth;       // Reference to the player's heatlh.
    public GameObject[] enemies;                // The enemy prefab to be spawned.
    public float spawnTime = 10f;            // How long between each spawn.
    public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
    GameObject player;
    


    public override void OnStartServer()
    {
        
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
		string gameType = PlayerPrefs.GetString("GameType");
	   if("SL".Equals(gameType)){
		   spawnTime = spawnTime/3;
	   }
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }


    void Spawn()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(player!= null){
            playerHealth = player.GetComponent<PlayerHealth>();

            // If the player has no health left...
            if (playerHealth != null && playerHealth.currentHealth <= 0f)
            {
                // ... exit the function.
                return;
            }
			GameObject enemy = enemies[Random.Range(0, enemies.Length)];

            // Find a random index between zero and one less than the number of spawn points.
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);

            // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
            GameObject e = (GameObject)Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            NetworkServer.Spawn(e);
        }
        
    }
}
