﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyManager : NetworkBehaviour {

    PlayerHealth playerHealth;       // Reference to the player's heatlh.
    public GameObject enemy;                // The enemy prefab to be spawned.
    public float spawnTime = 3f;            // How long between each spawn.
    public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
    GameObject player;
    


    // void Start()
    // {
    //     player = GameObject.FindGameObjectWithTag("Player");
    //     playerHealth = player.GetComponent<PlayerHealth>();
    //     // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
    //     InvokeRepeating("Spawn", spawnTime, spawnTime);
    // }


    // void Spawn()
    // {
    //     // If the player has no health left...
    //     if (playerHealth.currentHealth <= 0f)
    //     {
    //         // ... exit the function.
    //         return;
    //     }

    //     // Find a random index between zero and one less than the number of spawn points.
    //     int spawnPointIndex = Random.Range(0, spawnPoints.Length);

    //     // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
    //     Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    // }

    public override void OnStartServer()
    {
        Debug.Log("OnStartServer");

        // Find a random index between zero and one less than the number of spawn points.
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        GameObject e = (GameObject)Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        NetworkServer.Spawn(e);
    }


}
