using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AvatarSelection : MonoBehaviour {

    AudioSource uiAudio;
	
	public Button confirmButton;	
	public Button adButton;
	public AudioClip clickClip; 
	

    public GameObject[] playerAvatarsPrefab;
	private GameObject[] avatars;
    private int index = 0;

	void Awake() {
		uiAudio = GetComponent<AudioSource>();
		
        index = PlayerPrefs.GetInt("SelectedAvatar");
		avatars = new GameObject[playerAvatarsPrefab.Length];
		 
        for(int i=0; i<playerAvatarsPrefab.Length; i++)
        {
            GameObject p_a = Instantiate(playerAvatarsPrefab[i], new Vector3(0, 0, 0), Quaternion.Euler(0,50,0));
			p_a.SetActive(false);
			avatars[i] = p_a;
        }
        avatars[index].SetActive(true);
	}
	
	

    public void ToggleLeft()
    {
        avatars[index].SetActive(false);
        index--;
        if(index < 0)
        {
            index = avatars.Length - 1;
        }
        avatars[index].SetActive(true);
		EnableRewardButton(index);       
		playClickSound();
    }

    public void ToggleRight()
    {
        avatars[index].SetActive(false);
        index++;
        if (index >= avatars.Length)
        {
            index = 0;
        }
        avatars[index].SetActive(true);
		EnableRewardButton(index);
		playClickSound();
    }
	
	public void EnableRewardButton(int avatarIndex){
		if(avatarIndex == 0){
			confirmButton.enabled = true;
			adButton.gameObject.SetActive(false);
		}else{
			confirmButton.enabled = false;
			adButton.gameObject.SetActive(true);
		}
	}

    public void Confirm()
    {
		playClickSound();
        PlayerPrefs.SetInt("SelectedAvatar", index);
        SceneManager.LoadScene("StartScene");
    }
	
	public void Back(){
		playClickSound();
		SceneManager.LoadScene("StartScene");
	}
	
	
    void playClickSound() {
		uiAudio.clip = clickClip;
		uiAudio.Play();	
    }

}
