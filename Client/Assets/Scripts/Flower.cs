using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Flower : NetworkBehaviour
{
    [ClientRpc(Delivery = RpcDelivery.Reliable)]
    private void SendFlowerStateClientRPC(bool state)
    {
        this.gameObject.SetActive(state);
    }
}
