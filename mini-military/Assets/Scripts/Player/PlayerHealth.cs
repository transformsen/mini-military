using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerHealth : NetworkBehaviour {

	private NetworkStartPosition[] spawnPoints; 
    
    public Slider healthSlider;                                 // Reference to the UI's health bar.
    public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
    public AudioClip deathClip;                                 // The audio clip to play when the player dies.
	public AudioClip hurtClip;                                 // The audio clip to play when the player dies.
    public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.


    Animator anim;                                              // Reference to the Animator component.
    AudioSource playerAudio;                                    // Reference to the AudioSource component.
    PlayerMovement playerMovement;                              // Reference to the player's movement.
	ParticleSystem hitParticles;                // Reference to the particle system that plays when the player is damaged.
	public GameObject deathParticlesGO;                // Reference to the particle system that plays when the player is death.
	
    PlayerFire playerFire;                              // Reference to the PlayerShooting script.
    bool isDead;                                                // Whether the player is dead.
    bool damaged;                                               // True when the player gets damaged.

	public bool destroyOnDeath = false;
	
	public int startingHealth = 100;                            // The amount of health the player starts the game with.
	[SyncVar(hook = "OnChangeHealth")]
    public int currentHealth;
	
	[SyncVar]
	bool isSinking = false;                             // Whether the enemy has started sinking through the floor.
	public float sinkSpeed = 2.5f;              // The speed at which the enemy sinks through the floor when dead.
	
	[SyncVar]
    public string team;
    [SyncVar]
    public string playerName = "Soldier";
	[SyncVar]
    public int death = 0;
	
	public int power = 5;
	public Color poweredColor = Color.green;

    void Awake()
    {
		if (isLocalPlayer)
        {
            spawnPoints = FindObjectsOfType<NetworkStartPosition>();
			if(PlayerPrefs.GetInt("ExtraHealth") == 1 ){
				startingHealth = startingHealth + 10;
			}
        }
        // Setting up the references.
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
		playerMovement = GetComponent<PlayerMovement>();
		playerFire = GetComponent<PlayerFire>();
		hitParticles = GetComponentInChildren<ParticleSystem>();
		
		currentHealth = startingHealth;
    }


    void Update()
    {
		
		// If the enemy should be sinking...
        if (isSinking)
        {
            // ... move the enemy down by the sinkSpeed per second.
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }		
		
    }

	void OnChangeHealth(int health){
		healthSlider.value = health;		
	}
	
	
	
	[Server]
    public void TakeDamage(int amount, GameObject fromPlayer)
    {
		
		Debug.Log("Player TakeDamage"+amount);
		if (!isServer)
        {		
            return;
        }
         // Reduce the current health by the damage amount.
        currentHealth -= amount;

        // If the player has lost all it's health and the death flag hasn't been set yet...
        if (currentHealth <= 0)
        {
			AddScore(fromPlayer);
            // ... it should die.
            Death();			
        }
		
		RpcShowHitEffects();
		
    }
	
	[ClientRpc]
	void RpcShowHitEffects(){
		//StartCoroutine(ShowHitEffects());
		ShowHitEffects();
	}
	
	
	void ShowHitEffects(){
		if (isLocalPlayer)
        {
			//damageImage.color = flashColour;
			//yield return new WaitForSeconds(0.4f);
			//damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
			
			// Play the hurt sound effect.
			hitParticles.Play();
			playerAudio.clip = hurtClip;
			playerAudio.Play();		
		}
		//playerAudio.clip = hurtClip;
		//playerAudio.Play();		
	}
	
	//Adding the score for the player object who fiered. This will run only in server.
	[Server]
	void AddScore(GameObject fromPlayer){
		PlayerFire playerFired = fromPlayer.GetComponentInParent<PlayerFire>();
		if(playerFired != null){
			playerFired.AddScore(power, poweredColor);
		}
	}
	
	[Server]
    void Death()
    {   
		death++;
        // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
        playerAudio.clip = deathClip;
        playerAudio.Play();
		//deathParticlesGO.SetActive(true);
		
		
		EnableLocalPlayer(false);
		//Disable Player Effect on the Client
		RpcDisablePlayer();
		
		//Heath Back To Normal
		currentHealth = startingHealth;		
		
		StartCoroutine(ReSpawn());
		
    }
	
	[ClientRpc]
	void RpcDisablePlayer(){
		EnableLocalPlayer(false);
	}
	
	void EnableLocalPlayer(bool enable){
		if (isLocalPlayer)
        {
			deathParticlesGO.SetActive(!enable);
			playerMovement.enabled = enable;
			playerFire.enabled = enable;
			if(enable){
				playerFire.WeaponSwitch("Pistel");
			}
			StartCoroutine(InformScoreManger(enable));		
		}
		if(!enable){
			transform.position = new Vector3(0, -20f, 0);			
		}
	}
	
	IEnumerator InformScoreManger(bool enable){
		yield return new WaitForSeconds(0.1f);
		ScoreManager.isPlayerDeath = !enable;
	}
	
	IEnumerator ReSpawn(){
		yield return new WaitForSeconds(6f);
		EnableLocalPlayer(true);
		RpcRespawn();
	}
	
	[ClientRpc]
    void RpcRespawn()
    {
		EnableLocalPlayer(true);
		
        if (isLocalPlayer)
        {
           RespawnLocalPlayer();
        }
    }
	
	void RespawnLocalPlayer(){
		Debug.Log("RespawnLocalPlayer");
	
		// Set the spawn point to origin as a default value
		Vector3 spawnPoint = Vector3.zero;

		// If there is a spawn point array and the array is not empty, pick a spawn point at random
		if (spawnPoints != null && spawnPoints.Length > 0)
		{
			spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
		}

		// Set the player’s position to the chosen spawn point
		transform.position = spawnPoint;
	}
		
	
	public override void OnStartClient() {
		GameManager.RegisterPlayer(gameObject);
	}
}
