using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerPositionSync : NetworkBehaviour
{
    [SyncVar]
    private Vector3 syncPos;

    [SerializeField] Transform myTranform;
    [SerializeField] float lerpRate = 15f;

    // Update is called once per frame
    void FixedUpdate()
    {
        TransmitPosition();
        LerpPosition();
    }

    void LerpPosition(){
        if(!isLocalPlayer){
            myTranform.position =  Vector3.Lerp(myTranform.position, syncPos, Time.deltaTime * lerpRate);
        }
    }

    [Command]
    void CmdProvidePosotionToServer(Vector3 pos){
        syncPos = pos;
    }

    [ClientCallback]
    void TransmitPosition(){
        if(isLocalPlayer){
            CmdProvidePosotionToServer(myTranform.position);   
        }
    }
}
