using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerCamera : NetworkBehaviour
{
    [SerializeField] GameObject head;
    [SerializeField] GameObject playerBody;

    [ServerRpc(Delivery = RpcDelivery.Unreliable)]
    private void SendRotationToServerRPC(Vector3 eulerRotationValues, ulong clientID)
    {
        head.transform.localRotation = Quaternion.Euler(eulerRotationValues.x, 0f, 0f);
        playerBody.transform.rotation = Quaternion.Euler(0f, eulerRotationValues.y, 0f);
        SendRotationToClientRPC(eulerRotationValues, clientID);
    }


    //Will send to the host as well
    [ClientRpc(Delivery = RpcDelivery.Unreliable)]
    private void SendRotationToClientRPC(Vector3 eulerRotationValues, ulong clientID) 
    {
    }
}
