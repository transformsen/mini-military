using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyNetworkManager : NetworkManager {
   

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        int index = PlayerPrefs.GetInt("SelectedAvatar");
		Debug.Log("Picked Avatar=" + index);
        playerPrefab = spawnPrefabs[index];
        Transform spawnPoint = GetStartPosition();
        GameObject player = (GameObject)GameObject.Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }
}
