using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BombManager : NetworkBehaviour
{
    public float spawnTime = 20f;           // How long between each spawn.
    public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
    public GameObject bombPrefab;         // The enemy prefab to be spawned.
    public float lifeTime = 8f;             // Life Time of the Gun wait for player to pick.	
	bool spawnStart = false;
	
    public override void OnStartServer()
    {   
		if(!spawnStart){    
			InvokeRepeating("Spawn", spawnTime, spawnTime);
			spawnStart = true;
		}
    }


    void Spawn()
    {

		if(NetworkServer.active){
			// Find a random index between zero and one less than the number of spawn points.
			int spawnPointIndex = Random.Range(0, spawnPoints.Length);

			GameObject bomb = Instantiate(bombPrefab, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
				
			NetworkServer.Spawn(bomb);			
			Destroy(bomb, lifeTime);
		}
    }
}
