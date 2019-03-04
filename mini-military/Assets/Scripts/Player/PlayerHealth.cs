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
	public int score;        // The player's score.
	
	[SyncVar]
    public string team;
    [SyncVar]
    public string playerName;

    void Awake()
    {
		if (isLocalPlayer)
        {
            spawnPoints = FindObjectsOfType<NetworkStartPosition>();
        }
        // Setting up the references.
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerFire = GetComponent<PlayerFire>();
		
		currentHealth = startingHealth;
    }


    void Update()
    {
		
		
        // If the player has just been damaged...
        if (damaged)
        {
            // ... set the colour of the damageImage to the flash colour.
            damageImage.color = flashColour;
        }
        // Otherwise...
        else
        {
            // ... transition the colour back to clear.
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        // Reset the damaged flag.
        damaged = false;	

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
	
    public void TakeDamage(int amount)
    {
		
		Debug.Log("Player TakeDamage"+amount);
		if (!isServer)
        {
			Debug.Log("Server");			
            return;
        }
        // Set the damaged flag so the screen will flash.
        damaged = true;

        // Reduce the current health by the damage amount.
        currentHealth -= amount;

        // Play the hurt sound effect.
		playerAudio.clip = hurtClip;
        playerAudio.Play();

        // If the player has lost all it's health and the death flag hasn't been set yet...
        if (currentHealth <= 0)
        {
            // ... it should die.
            Death();
			score++;
        }
    }
	
	public void TakeDamage(GameObject fromPlayer){
		Debug.Log(fromPlayer);
		Debug.Log(fromPlayer.GetComponentInParent<PlayerFire>());
		fromPlayer.GetComponentInParent<PlayerFire>().AddScore();
	}


    void Death()
    {
		
		Debug.Log("Death");
		
        // Set the death flag so this function won't be called again.
        isDead = true;

        // Turn off any remaining shooting effects.
        //playerShooting.DisableEffects();

        // Tell the animator that the player is dead.
        //anim.SetTrigger("death");

        // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
        playerAudio.clip = deathClip;
        playerAudio.Play();

        // Turn off the movement and shooting scripts.
       //playerMovement.enabled = false;
       //playerShooting.enabled = false;

        //Drop weapon from hand
        //playerMovement.DetachWeapon();
		
        if (destroyOnDeath)
        {
            //Destroy(gameObject);
        }
		
		currentHealth = startingHealth;
		
		RpcRespawn();
    }
	
	
	
	[ClientRpc]
    void RpcRespawn()
    {
		Debug.Log("RpcRespawn");
		//anim.SetTrigger("death");
		//StartSinking();
		//gameObject.SetActive(false);
		transform.position = new Vector3(0, -20f, 0);
        if (isLocalPlayer)
        {
            Invoke("RespawnLocalPlayer", 10);
        }
    }
	
	void RespawnLocalPlayer(){
		Debug.Log("RespawnLocalPlayer");
		//GetComponent<Rigidbody>().isKinematic = false;
		//isSinking = false;
		//anim.ResetTrigger("death");
		//anim.SetBool("Reset", true);
		//gameObject.SetActive(true);
		
		isDead = false;
		
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
	
	public void StartSinking()
    {
       
        GetComponent<Rigidbody>().isKinematic = true;

        // The enemy should no sink.
        isSinking = true;

        
    }
	
	public override void OnStartClient() {
		GameManager.RegisterPlayer(gameObject);
	}
}
