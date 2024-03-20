using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;

public class NetworkManagerHelper : MonoBehaviour
{
    public static bool isRunning;

    private void Awake()
    {
        isRunning = false;
    }
    private void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;
        NetworkManager.Singleton.OnClientStopped += OnClientStopped;
    }


    private void OnClientConnected(ulong clientID) 
    {
        isRunning = true;
        UIStartOfGame.Singleton.SetInVisibleAndUnInteractable();
        
    }
    private void OnClientDisconnected(ulong clientID) 
    {
        if (NetworkManager.ServerClientId == clientID) isRunning = false;
        UIStartOfGame.Singleton.SetVisibleAndInteractable();
        UIEndOfGame.Singleton.SetInVisibleAndUnInteractable();

        switch (NetworkManager.Singleton.DisconnectReason) 
        {
            case "1":
                UIStartOfGame.Singleton.SetDisconnectText("Connection Error Please Try Again In A Few Minutes.");
                Debug.Log("Connection Error Please Try Again In A Few Minutes.");
                break;
            default:
                UIStartOfGame.Singleton.SetDisconnectText("Thanks for playing the game!");
                Debug.Log("Disconnected Via Timer!");
                PlayerPrefs.SetString("Time", System.DateTime.Now.ToString("o"));
                break;
        }

    }

    private void OnClientStopped(bool stopped) 
    {
        if (stopped == true) isRunning = false;
        UIStartOfGame.Singleton.SetVisibleAndInteractable();
        UIEndOfGame.Singleton.SetInVisibleAndUnInteractable();
    }

}
