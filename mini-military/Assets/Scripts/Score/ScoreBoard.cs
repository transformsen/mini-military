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

   [SerializeField]
   public RectTransform purchaseText;

   [SerializeField]
   public RectTransform gameOverText;
   
   [SerializeField]
   public RectTransform bonus;

   [SerializeField]
   public RectTransform scoredBoardCanvas;

   [SerializeField]
   public RectTransform winnerContainer;

   [SerializeField]
   public Text winnerText;

   public static int myscore = 0;

   
   void OnEnable(){
	   reSpawn.gameObject.SetActive(true);
		gameOverText.gameObject.SetActive(false);
		bonus.gameObject.SetActive(false);
		winnerContainer.gameObject.SetActive(false);	   
	   StartCoroutine(UpdateScoreBoard());	
	   if(GameManager.isGameOver){
			reSpawn.gameObject.SetActive(false);
			gameOverText.gameObject.SetActive(true);
			bonus.gameObject.SetActive(true);			
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
		int i =0;
		string winnerName = "";
		foreach(GameObject p in players){
			
			if (p !=null){				
				string name = p.GetComponent<PlayerHealth>().playerName;
				if(i == 0){
					winnerName = name;
					i++;
				}
				int score = p.GetComponent<PlayerFire>().score;
				int death = p.GetComponent<PlayerHealth>().death;
			
				GameObject playerScoreBoardItem = Instantiate(playerScoreBoardItemPrefab, playerScoreList);
				PlayerScoreBoardItem item = playerScoreBoardItem.GetComponent<PlayerScoreBoardItem>();
				if(item != null){
					item.SetUp(name, score, death);
				}
				myscore = score;
				if("SL".Equals(gameType)){
					int prevHighest = PlayerPrefs.GetInt("HighScore");
					if(score > prevHighest){
						if(GameManager.isGameOver){
							highScoreImage.gameObject.SetActive(true);
							highScoreText.text = ""+score;
							PlayerPrefs.SetInt("HighScore",score);
						}						
					}else{
						highScoreImage.gameObject.SetActive(false);
					}
					if(score < 1500){
						purchaseText.gameObject.SetActive(true);
					}else{
						purchaseText.gameObject.SetActive(false);
					}
				}else{
					highScoreImage.gameObject.SetActive(false);
					if(GameManager.isGameOver){
						winnerContainer.gameObject.SetActive(true);
						winnerText.text = "#Winner "+winnerName;
					}					
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
	   yield return new WaitForSeconds(10f);
	   GameManager.isGameOver = false;
	   ScoreManager.isPlayerDeath = false;
	   scoredBoardCanvas.gameObject.SetActive(false);	
	   gameOverText.gameObject.SetActive(false);
	   bonus.gameObject.SetActive(false);
	   string gameType = PlayerPrefs.GetString("GameType");
	   if("DM".Equals(gameType)){ //if(true){
		   LobbyManager.s_Singleton.ServerReturnToLobby();
		   //SendReturnToLobby
		   //SceneManager.LoadScene("LobbyScene");
	   }else{
		   SingleNetworkHUD.started = false;
		   NetworkManager.singleton.StopHost();
	   }
   }
 
}
