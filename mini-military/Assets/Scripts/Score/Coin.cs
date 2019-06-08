using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
	public int count = 1;
    // Start is called before the first frame update
   AudioSource pickupAudio;                     // Reference to the audio source.
	void Start(){
		pickupAudio = GetComponent<AudioSource>();
	}
	
	void Update () {
        transform.Rotate(new Vector3(45,0,0) * Time.deltaTime);
	}
	
    void OnTriggerEnter(Collider collision)
    {
        GameObject hit = collision.gameObject;
        PlayerBombAttack playerBombAttack = hit.GetComponent<PlayerBombAttack>();
		
        if (playerBombAttack != null)
        {
			pickupAudio.Play();
			int coins = PlayerPrefs.GetInt("Coins");				
			coins = coins + count;
			PlayerPrefs.SetInt("Coins", coins);
			Destroy(gameObject, 0.25f);
			
		}
    }
}
