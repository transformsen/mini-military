using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AvatarSelection : MonoBehaviour {

    private GameObject[] avatars;
    private int index = 0;

	void Awake() {
        index = PlayerPrefs.GetInt("SelectedAvatar");
        avatars = new GameObject[transform.childCount];
        for(int i=0; i<transform.childCount; i++)
        {
            GameObject avatar = transform.GetChild(i).gameObject;
            avatar.SetActive(false);
            avatars[i] = avatar;
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
        SceneManager.LoadScene("StartScene");
    }

}
