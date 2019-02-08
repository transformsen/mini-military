using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

    public float spawnTime = 20f;           // How long between each spawn.
    public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
    public GameObject weaponPrefab;         // The enemy prefab to be spawned.
    public float lifeTime = 8f;             // Life Time of the Gun wait for player to pick.

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
        if(weaponPrefab != null)
        {
            GameObject weapon = Instantiate(weaponPrefab, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);

            int weaponIndex = Random.Range(0, 5/*weapon.transform.childCount*/);
            weapon.gameObject.GetComponent<WeaponSwitch>().resetSelection();
            weapon.transform.GetChild(weaponIndex).gameObject.SetActive(true);
            weapon.gameObject.GetComponent<WeaponSwitch>().enableTrigger = true;

            // weapon.transform.SetPositionAndRotation(new Vector3(weapon.transform.position.x, 0f, weapon.transform.position.y),
            //     new Quaternion(weapon.transform.rotation.x, weapon.transform.rotation.y, weapon.transform.rotation.z, 0f));

            Destroy(weapon, lifeTime);
        }
        
    }
}
