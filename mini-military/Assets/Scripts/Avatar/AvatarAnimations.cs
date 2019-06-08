using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarAnimations : MonoBehaviour
{
	Animator anim;                      // Reference to the animator component.
    // Start is called before the first frame update
    void Start()
    {
         // Set up references.
        anim = GetComponent<Animator>();
		anim.speed = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
         anim.SetBool("isRunning", true);
    }
	
	void Step(){
		
	}
}
