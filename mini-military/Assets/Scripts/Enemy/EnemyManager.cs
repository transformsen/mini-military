using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Prototype.NetworkLobby;

public class EnemyManager : NetworkBehaviour {

    PlayerHealth playerHealth;       // Reference to the player's heatlh.
    public GameObject[] enemies;                // The enemy prefab to be spawned.
    public float spawnTime = 5f;            // How long between each spawn.
    public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
	public Transform[] spawnPointsGateOpen;         // An array of the spawn points this enemy can spawn from.
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
			
			
			int spawnPointIndex = 0;	
			Transform spawnPoint = transform;
			if(PlayerPrefs.GetInt("Mission3Done") == 1){
				// Find a random index between zero and one less than the number of spawn points.
				spawnPointIndex = Random.Range(0, spawnPointsGateOpen.Length);
				spawnPoint = spawnPointsGateOpen[spawnPointIndex];
			}else{
				// Find a random index between zero and one less than the number of spawn points.
				spawnPointIndex = Random.Range(0, spawnPoints.Length);
				spawnPoint = spawnPoints[spawnPointIndex];
			}
			
			GameObject enemy = enemies[Random.Range(0, enemies.Length)];
            

            // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
            
            Debug.Log(timeRan);

            if(!(timeRan > 100 && timeRan < 150 ) || (timeRan > 200 && timeRan < 250 ) 
            || (timeRan > 300 && timeRan < 350 ) || (timeRan > 400 && timeRan < 500 )
            || (timeRan > 700 && timeRan < 750 ) || (timeRan > 800 && timeRan < 850 ) 
            || (timeRan > 900 && timeRan < 950 ) || (timeRan > 1000 && timeRan < 1100 ) 
			|| (timeRan > 1200 && timeRan < 1250 ) 
            || (timeRan > 1300 && timeRan < 1350 ) || (timeRan > 1400 && timeRan < 1500 )
			|| (timeRan > 1550 && timeRan < 1600 ) 
            || (timeRan > 1620 && timeRan < 1800 )){
                GameObject e = (GameObject)Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
                NetworkServer.Spawn(e);
                Destroy(e, 200);
            }            
        }
        
    }
}
