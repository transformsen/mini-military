using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Prototype.NetworkLobby;

public class ScoreManager : MonoBehaviour {

 	public RectTransform scrodeCard;
	public static bool isPlayerDeath = false;

	void Start(){
		scrodeCard.gameObject.SetActive(false);
	}
	
    void Update()
    {
        
		if(isPlayerDeath){
			scrodeCard.gameObject.SetActive(true);
		}else{
			scrodeCard.gameObject.SetActive(false);
		}
		
    }
	
	public void Quit()
    {
		//NetworkManager.singleton.StopClient();
		//NetworkManager.singleton.StopHost();
		//NetworkTransport.Shutdown();
		//NetworkServer.DisconnectAll();
		//LobbyManager.s_Singleton.StopClient();
		//LobbyManager.s_Singleton.StopServer();
		//LobbyManager.s_Singleton.lobbyNetworkDiscovery.StopBroadcast();
		//SceneManager.LoadScene("StartScene");
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
    }

	public void goToBoosters(){
		SceneManager.LoadScene("PurchaseScene");
	}

	
}
