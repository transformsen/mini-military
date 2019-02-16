using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FirstAidManager : NetworkBehaviour {

    public float spawnTime = 20f;            // How long between each spawn.
    public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
    public GameObject firstAid;                // The enemy prefab to be spawned.
    public float lifeTime = 8f;

    public override void OnStartServer()
    {
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }


    void Spawn()
    {
        
        // Find a random index between zero and one less than the number of spawn points.
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
		GameObject firstAidKit = Instantiate(firstAid, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
		NetworkServer.Spawn(firstAidKit);
        Destroy(firstAidKit, lifeTime);
    }
}
