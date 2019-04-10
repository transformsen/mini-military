using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBooster : MonoBehaviour
{
	AudioSource pickupAudio;                     // Reference to the audio source.
	void Start(){
		pickupAudio = GetComponent<AudioSource>();
	}
	
    void OnTriggerEnter(Collider collision)
    {
        GameObject hit = collision.gameObject;
        PlayerBombAttack playerBombAttack = hit.GetComponent<PlayerBombAttack>();
		
        if (playerBombAttack != null)
        {
			pickupAudio.Play();
			playerBombAttack.numberOfBombs += 1;						
			Destroy(gameObject, 0.2f);
			
		}
    }
}
