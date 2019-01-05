using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBombAttack : MonoBehaviour {

    public GameObject bombPrefab;
    public Transform bombSpawn;
    public float timeBetweenBullets = .15f;        // The time between each shot.
    public float bombspeed = 9f;

    float timer;                                    // A timer to determine when to fire.

    // Update is called once per frame
    void Update () {
        
        timer += Time.deltaTime;
        if (Input.GetButtonDown("Fire3") && timer >= timeBetweenBullets)
        {
            Debug.Log("THrowing Bomb!");
            ThrowBomb();
        }

    }

    void ThrowBomb()
    {

        GameObject bomb = (GameObject)Instantiate(
           bombPrefab,
           bombSpawn.position,
           bombSpawn.rotation);
        Rigidbody rb = bomb.GetComponent<Rigidbody>();        
        rb.AddForce(transform.forward * bombspeed, ForceMode.VelocityChange);

    }
}
