using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;


public class NetworkManagerHelper : MonoBehaviour
{
    public static NetworkManagerHelper Singleton { get; private set; }
    [SerializeField] GameObject PlayerGameObject;
    public bool isPrefabSpawned { get; private set; }
    public static string CurrentClientUsername = "";
    ulong currentPlayerId;

    public static List<GameObject> gameObjectsToActivate = new List<GameObject>();
    

    private void Awake()
    {
        isPrefabSpawned = false;
        Singleton = this;
        timer = 300;
        actualTimer = timer;
    }

    private void Start()
    {
        timer = 300;
        NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;
        NetworkManager.Singleton.ConnectionApprovalCallback = ApprovalCheck;
        var AllNPCs = FindObjectsOfType<NPCBehaviour>();
        foreach (var npc in AllNPCs) 
        {
            NetworkManager.Singleton.OnClientDisconnectCallback += npc.OnClientDisconnected;
        }
        NetworkManager.Singleton.OnClientDisconnectCallback += FindObjectOfType<ChatGPTNPC>().OnClientDisconnected;
        gameObjectsToActivate.Add(FindObjectOfType<Flower>().gameObject);
        NetworkManager.Singleton.StartServer();
        
    }

    //fix timer
    float timer;
    public static float actualTimer;
    private void Update()
    {
        if (isPrefabSpawned) {
            if (actualTimer > 0)
            {
                actualTimer -= Time.deltaTime;
                SendTimeToClientRPC(actualTimer);
            }
            else
            {
                NetworkManager.Singleton.DisconnectClient(currentPlayerId);
            }
        }
    }

    private void ApprovalCheck(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
    {

        if (!isPrefabSpawned)
        {
            currentPlayerId = request.ClientNetworkId;
            Instantiate(PlayerGameObject, GameObject.FindGameObjectWithTag("Spawn").transform.position, Quaternion.identity).GetComponent<NetworkObject>().SpawnWithOwnership(currentPlayerId);
            isPrefabSpawned = true;
            response.Approved = true;
            actualTimer = timer; 
        }
        else
        {
            response.Approved = false;
            response.Reason = "1";
        }
    }


    private void OnClientDisconnected(ulong clientID) 
    {
        Debug.Log("Client has disconnected!");
        isPrefabSpawned = false;
        CurrentClientUsername = "";
        foreach (GameObject gameObject in gameObjectsToActivate) 
        {
            gameObject.SetActive(true);
            var scriptOnClientDisconnected = gameObject.GetComponent<IonClientDisconnected>();
            if (scriptOnClientDisconnected != null) scriptOnClientDisconnected.OnClientDisconnected(clientID);
        }
    }

    [ClientRpc(Delivery = RpcDelivery.Unreliable)]
    private void SendTimeToClientRPC(float time)
    {
        
    }
}