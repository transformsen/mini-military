using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour {

	AudioSource pickupAudio;                     // Reference to the audio source.
	void Start(){
		pickupAudio = GetComponent<AudioSource>();
	}
	
	void Update () {
        transform.Rotate(new Vector3(45,0,0) * Time.deltaTime);
	}
	
	public string gunName = "";
    
    void OnTriggerEnter(Collider collision)
    {
        GameObject hit = collision.gameObject;
        PlayerFire playerFire = hit.GetComponent<PlayerFire>();
		
        if (playerFire != null)
        {
			pickupAudio.Play();
			string activeWeapon = gunName;
			playerFire.WeaponSwitch(activeWeapon);
			Destroy(gameObject, 0.2f);
			
		}
    }

    string GetActiveWeaponName()
    {
        string weapon_Name = null;
        foreach (Transform w in transform)
        {
            if (w.gameObject.activeSelf == true)
            {
                weapon_Name = w.name;
                break;
            }            
        }
        return weapon_Name;
    }


    public void resetSelection()
    {
        foreach (Transform w in transform)
        {
			w.gameObject.SetActive(false);
            
		}
    }
}
