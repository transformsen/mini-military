﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
//using UnityStandardAssets.CrossPlatformInput;

public class PlayerBombAttack : NetworkBehaviour {

    public GameObject bombPrefab;
    public Transform bombSpawn;
    public float timeBetweenBullets = .15f;        // The time between each shot.
    public float bombspeed = 9f;
    public int numberOfBombs = 5;
    public Text bombCountText;

    float timer;                                    // A timer to determine when to fire.

    // Update is called once per frame
    void Update () {
        if (!isLocalPlayer)
        {
            // exit from update if this is not the local player
            return;
        }
        timer += Time.deltaTime;
        bombCountText.text = (numberOfBombs > 0) ? "" + numberOfBombs : "";
        if (Input.GetButtonDown("Fire3") && timer >= timeBetweenBullets && numberOfBombs >0)
        {
            numberOfBombs--;
            CmdThrowBomb();
            
        }

    }

	[Command]
    void CmdThrowBomb()
    {

        GameObject bomb = (GameObject)Instantiate(
           bombPrefab,
           bombSpawn.position,
           bombSpawn.rotation);
        Rigidbody rb = bomb.GetComponent<Rigidbody>();        
        rb.AddForce(transform.forward * bombspeed, ForceMode.VelocityChange);
		NetworkServer.Spawn(bomb);

    }
}
