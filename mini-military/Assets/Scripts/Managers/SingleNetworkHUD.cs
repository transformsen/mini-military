using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class SingleNetworkHUD : MonoBehaviour
{
	public float guiOffset;
	public static bool started;
	
    // Start is called before the first frame update
    void Start()
    {
        started = false;
    }
	
	void Update()
	{
		if (Input.GetKey(KeyCode.Escape))
		{
			exitToStart();
		}
	}
	

    public void OnGUI(){
				
		GUIStyle style = new GUIStyle(GUI.skin.button);
        style.normal.textColor = Color.white;
        style.active.textColor = Color.white;
        style.fontSize = 35;
		
		if(!started){
				GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
				GUILayout.FlexibleSpace();
				GUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();

				GUI.backgroundColor = Color.magenta;
				if(GUILayout.Button("Play!" , style , GUILayout.Width(200), GUILayout.Height(100))){
					started = true;
					NetworkManager.singleton.StartHost();					
				}
				GUILayout.FlexibleSpace();
				GUILayout.EndHorizontal();
				GUILayout.FlexibleSpace();
				GUILayout.EndArea();
			
		}		
	}
	
	public void exitToStart(){
		Debug.Log("exitToStart");
		NetworkManager.singleton.StopHost();
		NetworkTransport.Shutdown();
		Destroy(gameObject);
		SceneManager.LoadScene("StartScene");		
		
	}
}
