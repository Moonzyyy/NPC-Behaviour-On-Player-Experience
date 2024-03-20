using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;
using UnityEngine.Events;

public class PlayerInteraction : NetworkBehaviour
{

    [SerializeField] LayerMask interactable;
    private static string username;
    public static string Username => username;

    UnityEvent endOfConversationEvent = new UnityEvent();

    ChatGPTNPC chatGPTNPC;

    public static bool hasFlower;
    public static bool hasGivenFlower;

    private void Awake()
    {
        chatGPTNPC = FindObjectOfType<ChatGPTNPC>();
        hasFlower = false;
        interactable = LayerMask.GetMask("interactable");
    }

    [ServerRpc(Delivery = RpcDelivery.Unreliable)]
    private void SendInteractionToServerRPC()
    {
        Debug.Log("Player is trying to interact");
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 2.5f;
        Debug.DrawRay(transform.position, forward, Color.red, 5f);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 2.5f, interactable)) 
        {
            if (hit.transform.gameObject.tag == "NPC")
            {
                if (hit.transform.GetComponent<ChatGPTNPC>() != null) 
                {
                    ChatGPTLoadingScreenClientRPC();
                    chatGPTNPC.CallChatGPTFunction(this);
                }
                else
                {
                    Debug.Log("Player is interacting with NPC");
                    var npc = hit.transform.GetComponent<NPCBehaviour>();
                    var conversationArray = new ConversationArray(npc.StartConversation().ConversationList);
                    endOfConversationEvent.AddListener(npc.EndOfConversation);
                    Debug.Log("Conversation List Length:  " + conversationArray.conversations.Length);
                    InteractionRespondClientRPC(conversationArray);
                    npc.gameObject.transform.LookAt(this.gameObject.transform);
                }
            }
            //might need to increase the range of this
            else if (hit.transform.gameObject.GetComponent<Flower>() != null) 
            {
                Debug.Log("Picked up flower");
                hit.transform.gameObject.GetComponent<Flower>().PickUpLogic();
                hasFlower = true;
            }
          
        }
    }

    [ClientRpc(Delivery = RpcDelivery.Reliable)]
    private void ChatGPTLoadingScreenClientRPC()
    {

    }

    [ServerRpc(Delivery = RpcDelivery.Reliable)]
    private void DeleteLastChatGPTMessageServerRPC()
    {
        chatGPTNPC.chatGPTConversationList.RemoveAt(chatGPTNPC.chatGPTConversationList.Count - 1);
    }

    [ClientRpc(Delivery = RpcDelivery.Reliable)]
    private void InteractionRespondClientRPC(ConversationArray conversationArray) 
    {

    }

    public void SendChatGPTText(string text) 
    {
        InteractionRespondChatCPTClientRPC(text);
    }

    [ClientRpc(Delivery = RpcDelivery.Reliable)]
    private void InteractionRespondChatCPTClientRPC(string text)
    {
        
    }

    [ServerRpc(Delivery = RpcDelivery.Reliable)]
    private void SendUserNameToServerRPC(string username)
    {
        Debug.Log("Client with username: " + username + " has joined.");
        PlayerInteraction.username = username;
    }


    [ServerRpc(Delivery = RpcDelivery.Reliable)]
    private void SendTextToChatGPTNPCServerRPC(string text)
    {
        chatGPTNPC.chatGPTConversationList.Add($"\n{PlayerInteraction.Username}: {text}");
        chatGPTNPC.CallChatGPTFunction(this);
    }

    [ServerRpc(Delivery = RpcDelivery.Reliable)]
    private void EndOfConversationServerRPC() 
    {
        Debug.Log("End of conversation");
        endOfConversationEvent.Invoke();
        endOfConversationEvent.RemoveAllListeners();
    }
}

public class ConversationArray : INetworkSerializable
{
    public string[] conversations;

    public ConversationArray(List<string> conversationList) 
    {
        Debug.Log("Converting conversation list of size: " + conversationList.Count + " into an array.");
        conversations = conversationList.ToArray();
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        if (serializer.IsWriter)
        {
            int arrayLength = conversations.Length;
            serializer.GetFastBufferWriter().WriteValueSafe(arrayLength);
            for (int i = 0; i < conversations.Length; i++) serializer.GetFastBufferWriter().WriteValueSafe(conversations[i]);
        }
        else 
        {
            int arrayLength;
            serializer.GetFastBufferReader().ReadValueSafe(out arrayLength);
            conversations = new string[arrayLength];
            for (int i = 0; i < conversations.Length; i++) serializer.GetFastBufferReader().ReadValueSafe(out conversations[i]);
        }
    }
}
