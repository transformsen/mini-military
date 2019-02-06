using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    void Awake()
    {
    }

    public void goToMainMenu()
    {
        Debug.Log("LGo to start");
        SceneManager.LoadScene("StartScene");
        
    }
}
