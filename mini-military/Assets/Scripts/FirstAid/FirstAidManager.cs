using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAidManager : MonoBehaviour {

    public float spawnTime = 20f;            // How long between each spawn.
    public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
    public GameObject firstAid;                // The enemy prefab to be spawned.
    public float lifeTime = 8f;

    void Start()
    {
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }


    void Spawn()
    {
        
        // Find a random index between zero and one less than the number of spawn points.
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        Destroy(Instantiate(firstAid, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation), lifeTime);
    }
}
