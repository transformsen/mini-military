using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
	
    public static bool isGameOver = false;	
	public static ArrayList players = new ArrayList();
	public static void RegisterPlayer(GameObject player){
		players.Add(player);
		NetworkIdentity m_Identity = player.GetComponent<NetworkIdentity>();				
		Debug.Log("netId = "+m_Identity.netId);
	}
	public static ArrayList GetPlayers(){
		return players;
	}
	
	public static void GameOver(){
		//isGameOver = true;
		ScoreManager.isPlayerDeath = true;		
		foreach(GameObject p in players){
			p.SetActive(false);
		}
	}	
	
}
