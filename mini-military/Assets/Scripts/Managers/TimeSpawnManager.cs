using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TimeSpawnManager : NetworkBehaviour
{
	public GameObject timerPreFab;                // The enemy prefab to be spawned.
    
	 public override void OnStartServer()
    {
        GameObject timer = Instantiate(timerPreFab);
		NetworkServer.Spawn(timer);
    }
}
