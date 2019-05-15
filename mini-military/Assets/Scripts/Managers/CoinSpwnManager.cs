using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CoinSpwnManager : NetworkBehaviour
{
    public float spawnTime = 2f;            // How long between each spawn.
    public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
	public Transform[] spawnPointsGateOpen;         // An array of the spawn points this enemy can spawn from.
    public GameObject coinPrfab;                // The enemy prefab to be spawned.
    public float lifeTime = 10f;

    public override void OnStartServer()
    {       
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }


    void Spawn()
    {
		if(NetworkServer.active){        
			// Find a random index between zero and one less than the number of spawn points.
			Transform spawnPoint = transform;
			if(PlayerPrefs.GetInt(GateOpener.GateOpenMission) == 1){
				// Find a random index between zero and one less than the number of spawn points.								
				int spawnPointIndex = Random.Range(0, spawnPointsGateOpen.Length);				
				spawnPoint = spawnPointsGateOpen[spawnPointIndex];
			}else{
				// Find a random index between zero and one less than the number of spawn points.				
				int spawnPointIndex = Random.Range(0, spawnPoints.Length);
				spawnPoint = spawnPoints[spawnPointIndex];
			}
			
						// Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
			GameObject coin = Instantiate(coinPrfab, spawnPoint.position, spawnPoint.rotation);
			NetworkServer.Spawn(coin);
			Destroy(coin, lifeTime);
		}
    }
}
