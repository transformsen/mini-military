using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;
using System.Collections;

public class LobyyTetsManager : NetworkLobbyManager
{
	public GameObject myPlayer;
    
	void Start(){
		playerPrefab = myPlayer;
	}
}
