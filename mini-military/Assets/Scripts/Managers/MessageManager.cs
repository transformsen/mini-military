using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour
{
	public GameObject[] messages;
	
    // Start is called before the first frame update
    void Start()
    {
        int index = Random.Range(0, messages.Length);
		GameObject message = messages[index];
		message.SetActive(true);
    }
   
}
