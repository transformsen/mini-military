using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using Prototype.NetworkLobby;

public class ScoreBoard : MonoBehaviour
{
	
   [SerializeField]
   GameObject playerScoreBoardItemPrefab;
	
   [SerializeField]
   Transform playerScoreList;
   
   [SerializeField]
   public RectTransform reSpawn;
   
   [SerializeField]
   public RectTransform highScoreImage;
   
   [SerializeField]
   public Text highScoreText;

   
   void OnEnable(){	   
	   StartCoroutine(UpdateScoreBoard());	
	   if(GameManager.isGameOver){
			reSpawn.gameObject.SetActive(false);
			StartCoroutine(GoToLobby());
	   }
	}
   
   IEnumerator UpdateScoreBoard(){
	   reSpawn.gameObject.SetActive(true);
		yield return new WaitForSeconds(0.8f);
		ArrayList players = GameManager.GetPlayers();
		string gameType = PlayerPrefs.GetString("GameType");
		
		IComparer scoreComparator = new ScoreComparator();
		players.Sort( scoreComparator );
			
		foreach(GameObject p in players){
			
			if (p !=null){				
				string name = p.GetComponent<PlayerHealth>().playerName;
				int score = p.GetComponent<PlayerFire>().score;
				int death = p.GetComponent<PlayerHealth>().death;
			
				GameObject playerScoreBoardItem = Instantiate(playerScoreBoardItemPrefab, playerScoreList);
				PlayerScoreBoardItem item = playerScoreBoardItem.GetComponent<PlayerScoreBoardItem>();
				if(item != null){
					item.SetUp(name, score, death);
				}
				if("SL".Equals(gameType)){
					int prevHighest = PlayerPrefs.GetInt("HighScore");
					if(score > prevHighest){
						highScoreImage.gameObject.SetActive(true);
						highScoreText.text = ""+score;
						PlayerPrefs.SetInt("HighScore",score);
					}else{
						highScoreImage.gameObject.SetActive(false);
					}
				}else{
					highScoreImage.gameObject.SetActive(false);
				}
			}				
		}
   }
   
   void OnDisable(){
	   foreach(Transform child in playerScoreList){
		   Destroy(child.gameObject);
	   }
   }
   
   IEnumerator GoToLobby(){
	   yield return new WaitForSeconds(8f);
	   GameManager.isGameOver = false;
	   ScoreManager.isPlayerDeath = false;	
	   string gameType = PlayerPrefs.GetString("GameType");
	   if("DM".Equals(gameType)){
		   LobbyManager.s_Singleton.SendReturnToLobby();
	   }else{
		   SingleNetworkHUD.started = false;
		   NetworkManager.singleton.StopHost();
	   }
   }
 
}
