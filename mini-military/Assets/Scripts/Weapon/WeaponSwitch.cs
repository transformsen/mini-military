using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour {

    // Use this for initialization
    GameObject player;                          // Reference to the player GameObject.
    PlayerMovement playerMovement;
    public string weaponName = null;
    public bool enableTrigger = false;

    void Awake()
    {
        // Setting up the references.
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        SwitchWeapon(weaponName);
    }

    // Update is called once per frame
    void Update () {

        
    }

    void OnTriggerEnter(Collider other)
    {
        // If the entering collider is the player...
        
        if (enableTrigger && other.gameObject == player)
        {
            Debug.Log("PLayer In Range");
            string activeWeapon = GetActiveWeaponName();
            Debug.Log("PLayer In Range activeWeapon"+ activeWeapon);
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
        if(weaponName != null)
        {
            foreach (Transform w in transform)
            {                
                if (weaponName.Equals(w.name))
                {                    
                    w.gameObject.SetActive(true);
                }
                else
                {
                    w.gameObject.SetActive(false);
                }

            }
        }        
    }

    public void resetSelection()
    {
        foreach (Transform w in transform)
        {
           w.gameObject.SetActive(false);
           
        }

    }
}
