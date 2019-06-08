using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    // Start is called before the first frame update
	public int attackDamage = 100;
	
	
    void OnTriggerEnter(Collider collision)
    {
		GameObject hit = collision.gameObject;
        PlayerHealth playerHealth = hit.GetComponent<PlayerHealth>();
		
        if (playerHealth != null)
        {
			playerHealth.TakeDamage(attackDamage, gameObject); 
			
		}
        
    }
}
