using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

public class PlayerFire : NetworkBehaviour
{
	public WeaponSettingsDictionary weaponBareel;
	
	GameObject rightHandContainer;      // Right hand container to hold weapon
	GameObject weaponObj;
	
	Animator anim;                      // Reference to the animator component.
	
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
	
	public string activeWeaponName = "Pistel";
	
	int numberOfBulletsLeft = 0;              // Number bullets per load
	
	GameObject barrelPreFab;   
    int damagePerShot = 20;                  // The damage inflicted by each bullet.
    float timeBetweenBullets = 0.15f;        // The time between each shot.
    float range = 100f;                      // The distance the gun can fire.   
    int totalNumberOfBullets = 25;      
    Sprite imageforWeanpon;

	
    void Awake()
    {
		// Set up references.
        anim = GetComponent<Animator>();
		
        rightHandContainer = transform.
                                Find("Bip001").
                                Find("Bip001 Pelvis").
                                Find("Bip001 Spine").
                                Find("Bip001 R Clavicle").
                                Find("Bip001 R UpperArm").
                                Find("Bip001 R Forearm").
                                Find("Bip001 R Hand").
                                Find("R_hand_container").gameObject;

        weaponObj = rightHandContainer.transform.                                
                                Find("GunSpwnPoint").
                                Find("Weapons").gameObject;
		
		SetPower(activeWeaponName);

    }

    // Update is called once per frame
    void Update()
    {
		
		if (!isLocalPlayer)
        {
            // exit from update if this is not the local player
            return;
        }
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // If the Fire2 button is being press and it's time to fire...
        if (Input.GetButtonDown("Fire2") && timer >= timeBetweenBullets)
        {
			Debug.Log("Fire");
            ShootAnim(true);
            // ... shoot the gun only if it has bullets.
            if (numberOfBulletsLeft  > 0)
            {
                CmdShoot();
            }
        }else{
			ShootAnim(false);
		}
    }
	
	[Command]
	void CmdShoot(){
		
		
		Transform barrelEnd = weaponObj.transform.                                
                                Find(activeWeaponName).
                                Find("BarrelEnd").gameObject.transform;
		
		GameObject fireObject = (GameObject)Instantiate(
           barrelPreFab,
           barrelEnd.position,
           barrelEnd.rotation);
        
		NetworkServer.Spawn(fireObject);
		
		//Bullets wasted
        numberOfBulletsLeft--;

        // Reset the timer.
        timer = 0f;
		
		
	
		ParticleSystem gunParticles = fireObject.GetComponent<ParticleSystem>();
        LineRenderer gunLine = fireObject.GetComponent<LineRenderer>();
        AudioSource gunAudio = fireObject.GetComponent<AudioSource>();
        Light gunLight = fireObject.GetComponent<Light>();

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
        shootRay = new Ray(barrelEnd.position, barrelEnd.forward);
        
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
			
			// Try and find an EnemyHealth script on the gameobject hit.
            PlayerHealth playerHealth = shootHit.collider.GetComponent<PlayerHealth>();
			Debug.Log("Player playerHealth"+playerHealth);
            // If the EnemyHealth component exist...
            if (playerHealth != null)
            {
                Debug.Log("Player InRange");
				// ... the enemy should take damage.
                playerHealth.TakeDamage(damagePerShot);
				
            }else{
				Debug.Log("Player NULL");
			}
			
            // Set the second position of the line renderer to the point the raycast hit.
            gunLine.SetPosition(1, shootHit.point);
        }
        // If the raycast didn't hit anything on the shootable layer...
        else
        {
			 Debug.Log("Player out of Range");
            // ... set the second position of the line renderer to the fullest extent of the gun's range.
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
		
		
	}
	
	public void ShootAnim(bool enable)
    {
		if(anim != null){
			anim.SetBool("isShooting", enable);
		}
        
    }
	
	void SetPower(string weaponName){
		
		numberOfBulletsLeft = weaponBareel[weaponName].totalNumberOfBullets;
		barrelPreFab = weaponBareel[weaponName].barrelPreFab;   
		damagePerShot = weaponBareel[weaponName].damagePerShot;                 
		timeBetweenBullets = weaponBareel[weaponName].timeBetweenBullets;        
		range = weaponBareel[weaponName].range;                      
		totalNumberOfBullets = weaponBareel[weaponName].totalNumberOfBullets;      
		imageforWeanpon = weaponBareel[weaponName].imageforWeanpon;
	}
	
	
	public void WeaponSwitch(string weaponName)
    {
		activeWeaponName = weaponName;
       foreach (Transform w in weaponObj.transform)
       {                
                if (weaponName.Equals(w.name)){
                    w.gameObject.SetActive(true);
				}
                else{   
					w.gameObject.SetActive(false);
                }
        }        
    }
}
