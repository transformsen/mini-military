using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;                // The enemy prefab to be spawned.
    
    void Awake()
    {
        Spawn();
        
    }

    // Update is called once per frame
    void Spawn()
    {
        Instantiate(player, transform.position, transform.rotation);
    }
}
