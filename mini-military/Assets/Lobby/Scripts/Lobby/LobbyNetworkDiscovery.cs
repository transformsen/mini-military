using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Prototype.NetworkLobby;

public class LobbyNetworkDiscovery : NetworkDiscovery
{
    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
		Debug.Log("LobbyNetworkDiscovery  - fromAddress"+fromAddress);
		LobbyManager.s_Singleton.networkAddress = fromAddress;
        LobbyManager.s_Singleton.StartClient();
		StopBroadcast();
    }
}
