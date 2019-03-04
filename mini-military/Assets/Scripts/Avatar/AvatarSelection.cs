using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AvatarSelection : MonoBehaviour {

    public Button confirmButton;

    public GameObject[] playerAvatarsPrefab;
	private GameObject[] avatars = new GameObject[2];	
    private int index = 0;

	void Awake() {
        index = PlayerPrefs.GetInt("SelectedAvatar");
        for(int i=0; i<playerAvatarsPrefab.Length; i++)
        {
            GameObject p_a = Instantiate(playerAvatarsPrefab[i], new Vector3(0, 0, 0), Quaternion.identity);
			p_a.SetActive(false);
			avatars[i] = p_a;
        }
        avatars[index].SetActive(true);
        if(confirmButton != null){
            confirmButton.enabled = false;
        }
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
    }

    public void Confirm()
    {
        PlayerPrefs.SetInt("SelectedAvatar", index);
        SceneManager.LoadScene("LobbyScene");
    }

}
