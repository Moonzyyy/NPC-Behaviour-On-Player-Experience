using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Flower : NetworkBehaviour, IonClientDisconnected
{

    private void Start()
    {
        NetworkManagerHelper.gameObjectsToActivate.Add(this.gameObject);
    }
    public void OnClientDisconnected(ulong clientID)
    {
        PlayerInteraction.hasFlower = false;
        PlayerInteraction.hasGivenFlower = false;
        SendFlowerStateClientRPC(true);
    }

    public void PickUpLogic() 
    {
        this.gameObject.SetActive(false);
        PlayerInteraction.hasFlower = true;
        SendFlowerStateClientRPC(false);
    }

    [ClientRpc(Delivery = RpcDelivery.Reliable)]
    private void SendFlowerStateClientRPC(bool state)
    {

    }


}
