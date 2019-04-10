using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumnManager : MonoBehaviour
{
	public AudioMixer audioMixer;
	
    public void SetVolumn(float volumn){
		audioMixer.SetFloat("Volumn", volumn);
	}
}
