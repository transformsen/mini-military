using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponManager : NetworkBehaviour {

    public float spawnTime = 20f;           // How long between each spawn.
    public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
    public GameObject weaponPrefab;         // The enemy prefab to be spawned.
    public float lifeTime = 8f;             // Life Time of the Gun wait for player to pick.
	public GameObject[] gunPrefabs;
	

    public override void OnStartServer()
    {
		
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }


    void Spawn()
    {

		if(NetworkServer.active){
			// Find a random index between zero and one less than the number of spawn points.
			int spawnPointIndex = Random.Range(0, spawnPoints.Length);

			GameObject weapon = Instantiate(gunPrefabs[Random.Range(0, gunPrefabs.Length)], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
				
			NetworkServer.Spawn(weapon);			
			Destroy(weapon, lifeTime);
		}
    }
	
}
