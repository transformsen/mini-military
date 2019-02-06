using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour {

    public string weaponName = null;
    public bool enableTrigger = false;

    GameObject player;                          // Reference to the player GameObject.
    PlayerMovement playerMovement;

    void Awake()
    {
        //TODO: How this will work in Multi player Networking?
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        SwitchWeapon(weaponName);
    }

   

    void OnTriggerEnter(Collider other)
    {
        // If the entering collider is the player...
        
        if (enableTrigger && other.gameObject == player)
        {
            string activeWeapon = GetActiveWeaponName();
            playerMovement.SwitchWeapon(activeWeapon);
            Destroy(gameObject);
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


    public void SwitchWeapon(string weaponName)
    {
        if(weaponName != null && (!weaponName.Equals("Backup") || !weaponName.Equals("GunStatusCanvas")))
        {
            foreach (Transform w in transform)
            {                
                if (weaponName.Equals(w.name))
                {                    
                    if(w != null && w.gameObject != null && w.Find("BarrelEnd") != null && w.Find("BarrelEnd").GetComponent<PlayerShooting>() != null){
                        w.gameObject.SetActive(true);
                        w.Find("BarrelEnd").GetComponent<PlayerShooting>().isActiveWeapon = true;
                        w.Find("Rings").gameObject.SetActive(false);
                    }

                }
                else
                {
                    if(w != null && w.gameObject != null && w.Find("BarrelEnd") != null && w.Find("BarrelEnd").GetComponent<PlayerShooting>() != null){
                        w.gameObject.SetActive(false);
                        w.Find("BarrelEnd").GetComponent<PlayerShooting>().isActiveWeapon = false;
                        w.Find("Rings").gameObject.SetActive(true);
                    }
                }

            }
        }        
    }

    public void resetSelection()
    {
        foreach (Transform w in transform)
        {
            if(w != null && w.Find("BarrelEnd") != null && w.Find("BarrelEnd").GetComponent<PlayerShooting>() != null){
                // string weaponName = w.name;
                // if(!weaponName.Equals("Backup") || !weaponName.Equals("GunStatusCanvas")){
                    w.gameObject.SetActive(false);
                    w.Find("BarrelEnd").GetComponent<PlayerShooting>().isActiveWeapon = false;
                //}
            }           
        }

    }
}
