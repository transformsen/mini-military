using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Prototype.NetworkLobby;

public class EnemyManager : NetworkBehaviour {

    PlayerHealth playerHealth;       // Reference to the player's heatlh.
    public GameObject[] enemies;                // The enemy prefab to be spawned.
    public float spawnTime = 11f;            // How long between each spawn.
    public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
    GameObject player;
    float timeRan = 0.0f;
    [SerializeField]
    public RectTransform scoredBoardCanvas;

    public override void OnStartServer()
    {
        scoredBoardCanvas.gameObject.SetActive(false);
        ScoreManager.isPlayerDeath = false;
        GameManager.isGameOver = false;
        
        bool needEnemy = true;
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
	    string gameType = PlayerPrefs.GetString("GameType");
        timeRan = 0.0f;
	   if("SL".Equals(gameType)){           
		   spawnTime = 3.3f;
	   }

       if("DM".Equals(gameType)){
		   if(LobbyManager.s_Singleton._playerNumber > 2){
               needEnemy = false;
           }
	   }

       if(needEnemy){
           InvokeRepeating("Spawn", spawnTime, spawnTime);
       }       
    }

    void Update(){
        timeRan += Time.deltaTime;        
    }


    void Spawn()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(player!= null){
            playerHealth = player.GetComponent<PlayerHealth>();

            // If the player has no health left...
            if (playerHealth != null && playerHealth.currentHealth <= 0f)
            {
                // ... exit the function.
                return;
            }
			GameObject enemy = enemies[Random.Range(0, enemies.Length)];

            // Find a random index between zero and one less than the number of spawn points.
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);

            // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
            
            Debug.Log(timeRan);

            if(!(timeRan > 100 && timeRan < 120 ) || (timeRan > 200 && timeRan < 230 ) 
            || (timeRan > 400 && timeRan < 460 ) || (timeRan > 580 && timeRan < 640 ) ||
            (timeRan > 700 && timeRan < 720 )){
                GameObject e = (GameObject)Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
                NetworkServer.Spawn(e);
                Destroy(e, 200);
            }            
        }
        
    }
}
