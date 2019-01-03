using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplosion : MonoBehaviour {

    public float delay = 3f;
    public GameObject explosionEffect;
    public float radius = 5f;
    public float fource = 300f;

    bool hasExploeded = false;
    float countDown;
	// Use this for initialization
	void Start () {
        countDown = delay;

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
        
        Instantiate(explosionEffect, transform.position, transform.rotation);

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
                    enemyHealth.TakeDamage(100, new Vector3(0, 0, 0));
                }                
            }
        }
        
        Destroy(this.gameObject);
    }
}
