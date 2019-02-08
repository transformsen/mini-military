using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerShooting : MonoBehaviour {

    public int damagePerShot = 20;                  // The damage inflicted by each bullet.
    public float timeBetweenBullets = 0.15f;        // The time between each shot.
    public float range = 100f;                      // The distance the gun can fire.
    public int numberOfBullets = 0;                // Number bullets per load

    public bool isActiveWeapon = false;
    public int totalBullets = 25;
    public Text totalBulletsText;
    public Text currentBulletsText;
    public Text realoadingInText;
    public Image weapon2D;
    public Sprite imageforWeanpon;
    public string weaponName;

    public GameObject crossHiarPrefab;
    GameObject crossHair;

    int reloadIntervel = 30;                        // Time Intervel between each reload;
    PlayerMovement playerMovement;
    GameObject player;   

    float timer;                                    // A timer to determine when to fire.
    Ray shootRay;                                   // A ray from the gun end forwards.
    RaycastHit shootHit;                            // A raycast hit to get information about what was hit.
    int shootableMask;                              // A layer mask so the raycast only hits things on the shootable layer.
    ParticleSystem gunParticles;                    // Reference to the particle system.
    LineRenderer gunLine;                           // Reference to the line renderer.
    AudioSource gunAudio;                           // Reference to the audio source.
    Light gunLight;                                 // Reference to the light component.
    float effectsDisplayTime = 0.2f;                // The proportion of the timeBetweenBullets that the effects will display for.
    System.DateTime reloadStartTime = System.DateTime.Now;

    void Awake()
    {
        // Create a layer mask for the Shootable layer.
        shootableMask = LayerMask.GetMask("Shootable");

        // Set up the references.
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();

        numberOfBullets = totalBullets;

        //TODO: How will this work in multi player Networking?
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        
        if(crossHiarPrefab!= null)
        {
            
            if(crossHair == null)
            {
                crossHair = Instantiate(crossHiarPrefab);
            }
            ToggleCrossHair(false);
        }

    }

    void Update()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // If the Fire2 button is being press and it's time to fire...
        if (CrossPlatformInputManager.GetButton("Fire2")   && timer >= timeBetweenBullets && isActiveWeapon)
        {
            playerMovement.ShootAnim(true);
            // ... shoot the gun only if it has bullets.
            if (numberOfBullets > 0)
            {
                Shoot();
            }
        }
        else
        {
            playerMovement.ShootAnim(false);
        }

        // If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            // ... disable the effects.
            DisableEffects();
        }

        if (isActiveWeapon)
        {
            if (numberOfBullets >= 0)
            {
                //TODO: How to get the reference If this beacme prefab? 
                if(currentBulletsText != null){
                    currentBulletsText.text = "" + numberOfBullets;
                }
                
            }

            if(totalBulletsText != null){
                totalBulletsText.text = "" + totalBullets;

            }

            
            //TODO: How to get the reference If this beacme prefab? 
            if (weapon2D != null)
            {
                weapon2D.sprite = imageforWeanpon;
            }
            
        }
        

        reloadGun();

        if (!isActiveWeapon)
        {
            ToggleCrossHair(false);
        }

        positionCrossHair();

    }

    public void ToggleCrossHair(bool enabled)
    {
        enabled = isActiveWeapon && enabled;
        if (crossHair != null)
        {
            crossHair.SetActive(enabled);
        }
    }


    //TODO: find the better way. Instanitate everytime Or Active FAlse. 
    public void positionCrossHair()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        Transform spawn = transform;
        Vector3 spwnPosition = spawn.position;
        Vector3 dir = ray.GetPoint(range);
        if (Physics.Raycast(ray, out hit, range, shootableMask))
        {
            if (crossHair != null)
            {
                ToggleCrossHair(true);
                crossHair.transform.position = hit.point;
                crossHair.transform.LookAt(Camera.main.transform);
            }
        }
        else
        {
            ToggleCrossHair(false);
        }

    }

    public void DisableEffects()
    {
        // Disable the line renderer and the light.
        gunLine.enabled = false;
        gunLight.enabled = false;
    }

    void Shoot()
    {
        

        //Bullets wasted
        numberOfBullets--;

        // Reset the timer.
        timer = 0f;

        // Play the gun shot audioclip.
        gunAudio.Play();

        // Enable the light.
        gunLight.enabled = true;
        //gunTrail.enabled = true;

        // Stop the particles from playing if they were, then start the particles.
        gunParticles.Stop();
        gunParticles.Play();

        // Enable the line renderer and set it's first position to be the end of the gun.
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
        shootRay = new Ray(transform.position, transform.forward);
        
        // Perform the raycast against gameobjects on the shootable layer and if it hits something...
        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            
            // Try and find an EnemyHealth script on the gameobject hit.
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

            // If the EnemyHealth component exist...
            if (enemyHealth != null)
            {
                // ... the enemy should take damage.
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);
            }

            // Set the second position of the line renderer to the point the raycast hit.
            gunLine.SetPosition(1, shootHit.point);
        }
        // If the raycast didn't hit anything on the shootable layer...
        else
        {
            // ... set the second position of the line renderer to the fullest extent of the gun's range.
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }

    void OnDestroy()
    {
        //TODO : During Perfromace tuning...
        //{
        //    Destroy(crossHair);
        //}
    }

    public void reloadGun()
    {
        System.DateTime currentTime = System.DateTime.Now;
        int timeDiffBetweenReloads = (currentTime - reloadStartTime).Seconds;
        if(numberOfBullets <= 0)
        {
            if(realoadingInText != null){
                realoadingInText.text = "RELOADING IN " + (reloadIntervel - timeDiffBetweenReloads);
            }
            
        }
        
        if (timeDiffBetweenReloads > reloadIntervel)
        {
            numberOfBullets = totalBullets;
            reloadStartTime = currentTime;
            if(realoadingInText != null){
                realoadingInText.text = "";
            }
        }        
    }
}
