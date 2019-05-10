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
		PlayerPrefs.SetInt(ExtraPowersAd.weapomConstName+"SAK47", 1);
		PlayerPrefs.SetInt(ExtraPowersAd.weapomConstName+"SFlameThrower", 1);
		PlayerPrefs.SetInt(ExtraPowersAd.weapomConstName+"SPistel", 1);
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }


    void Spawn()
    {

		if(NetworkServer.active){
			// Find a random index between zero and one less than the number of spawn points.
			int spawnPointIndex = Random.Range(0, spawnPoints.Length);
			GameObject randomWeapon = gunPrefabs[Random.Range(0, gunPrefabs.Length)];
			int isSelected = PlayerPrefs.GetInt(ExtraPowersAd.weapomConstName+randomWeapon.name);
			if(isSelected == 1){
				GameObject weapon = Instantiate(randomWeapon, spawnPoints[spawnPointIndex].position, 
				spawnPoints[spawnPointIndex].rotation);
				
				NetworkServer.Spawn(weapon);			
				Destroy(weapon, lifeTime);
			}
			
		}
    }
	
}
