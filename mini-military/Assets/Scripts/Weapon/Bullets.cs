using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
	
	public int damagePerShot = 20;                  // The damage inflicted by each bullet.
    public float range = 100f;                      // The distance the gun can fire.   
   
   
	Ray shootRay;                                   // A ray from the gun end forwards.
	RaycastHit shootHit;                            // A raycast hit to get information about what was hit.
	int shootableMask;                              // A layer mask so the raycast only hits things on the shootable layer.
	ParticleSystem gunParticles;                    // Reference to the particle system.
	LineRenderer gunLine;                           // Reference to the line renderer.
	AudioSource gunAudio;                           // Reference to the audio source.
	Light gunLight;                                 // Reference to the light component.
	public GameObject myparent;
   
	
    // Start is called before the first frame update
    public void Fire()
    {
		shootableMask = LayerMask.GetMask("Shootable");
		
		Debug.Log("parent = "+ myparent);
		
        Debug.Log("Bullets Awake");
		ParticleSystem gunParticles = GetComponent<ParticleSystem>();
        LineRenderer gunLine = GetComponent<LineRenderer>();
        AudioSource gunAudio = GetComponent<AudioSource>();
        Light gunLight = GetComponent<Light>();

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
                enemyHealth.TakeDamage(damagePerShot, shootHit.point, myparent);
				//enemyHealth.TakeDamage(gameObject);
            }
			
			// Try and find an EnemyHealth script on the gameobject hit.
            PlayerHealth playerHealth = shootHit.collider.GetComponent<PlayerHealth>();
			Debug.Log("Player playerHealth"+playerHealth);
			Debug.Log("Player shootHit.collider"+shootHit.collider);
            // If the EnemyHealth component exist...
            if (playerHealth != null)
            {
                Debug.Log("Player InRange");
				// ... the enemy should take damage.
                playerHealth.TakeDamage(damagePerShot, myparent);
				//playerHealth.TakeDamage(myparent);
				
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
		Debug.Log("Bullets Awake end");
    }

    // Update is called once per frame
    void Awake()
    {
		Debug.Log("Bullets Start");
        Destroy(gameObject, .4f);
    }
}
