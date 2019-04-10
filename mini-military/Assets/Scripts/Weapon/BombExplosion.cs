using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplosion : MonoBehaviour {

    public float delay = 3f;
    public GameObject explosionEffectPreFab;
    public float radius = 5f;
    public float fource = 300f;
    public float lifeTime = 0.8f;
	public GameObject myparent;
    public int damageValue = 300;

    AudioSource blastAudio;
    bool hasExploeded = false;
    float countDown;
	// Use this for initialization
	void Start () {
        countDown = delay;
        blastAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate () {
        countDown -= Time.deltaTime;
        if(countDown <= 0f && !hasExploeded)
        {
            Explode();
            hasExploeded = true;
        }

    }

    void Explode()
    {
		
        if(explosionEffectPreFab!= null){
            GameObject explosionEffect = Instantiate(explosionEffectPreFab, transform.position, transform.rotation);
            Destroy(explosionEffect, 3f);
        }

        blastAudio.Play();

        Collider [] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach(Collider neearByCollider in colliders)
        {
            Rigidbody rb = neearByCollider.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(fource, transform.position, radius);
                EnemyHealth enemyHealth = neearByCollider.GetComponent<EnemyHealth>();
                if(enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damageValue, new Vector3(0, 0, 0), myparent);
					//enemyHealth.TakeDamage(myparent);
                }
				PlayerHealth playerHealth = neearByCollider.GetComponent<PlayerHealth>();
                if(playerHealth != null)
                {
                    playerHealth.TakeDamage(damageValue, myparent);
					//playerHealth.TakeDamage(myparent);
                } 				
            }
        }
        
        Destroy(this.gameObject, lifeTime);
        
    }
}
