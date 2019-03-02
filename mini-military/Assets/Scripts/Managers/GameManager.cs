using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public static ArrayList players = new ArrayList();
	public static void RegisterPlayer(GameObject player){
		players.Add(player);
		NetworkIdentity m_Identity = player.GetComponent<NetworkIdentity>();				
		Debug.Log("netId = "+m_Identity.netId);
	}
	public static ArrayList GetPlayers(){
		return players;
	}
}
