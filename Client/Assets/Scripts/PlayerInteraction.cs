using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.UIElements;

public class PlayerInteraction : NetworkBehaviour
{
    
    [SerializeField] GameObject TextBox;
    [SerializeField] ChatGPTBoxManager ChatGPTTextBox;

    [SerializeField] GameObject loadingScreen; 


    public static bool isInConversation;

    private void Awake()
    {
        isInConversation = false;
        loadingScreen.SetActive(false);
    }

    private void Start()
    {
        SendUserNameToServerRPC(UIStartOfGame.submittedUsername);
    }


    [ServerRpc(Delivery = RpcDelivery.Unreliable)]
    private void SendInteractionToServerRPC()
    {
       
    }

    [ClientRpc(Delivery = RpcDelivery.Reliable)]
    private void InteractionRespondClientRPC(ConversationArray conversationArray)
    {
        Debug.Log("Starting Conversation with NPC. Conversation Length: " + conversationArray.conversations.Length);
        StartCoroutine(Conversation(conversationArray));
    }

    [ClientRpc(Delivery = RpcDelivery.Reliable)]
    private void InteractionRespondChatCPTClientRPC(string text) 
    {
        StartCoroutine(ConversationChatGPTNPC(text));
    }

    public void LeftClick(InputAction.CallbackContext callbackContext) 
    {
        if (!callbackContext.started || isInConversation) return;
        SendInteractionToServerRPC();
    }

    private IEnumerator ConversationChatGPTNPC(string text) 
    {
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
        loadingScreen.SetActive(false);
        ChatGPTTextBox.SetActive();
        ChatGPTTextBox.SetChatGPTTextBox(text);
        yield return WaitForKeyPressedEnter();
        if (doneReturn)
        {
            ChatGPTTextBox.gameObject.SetActive(false);
            loadingScreen.SetActive(true);
            SendTextToChatGPTNPCServerRPC(ChatGPTTextBox.ReturnInputField.text);
        }
        else 
        {
            DeleteLastChatGPTMessageServerRPC();
            ChatGPTTextBox.gameObject.SetActive(false);
            isInConversation = false;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;     
        }
        
    }

    bool doneReturn;
    bool doneEscape;

    private IEnumerator WaitForKeyPressedEnter() 
    {
        doneReturn = false;
        doneEscape = false;
        while (!doneEscape && !doneReturn) 
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                 doneReturn = true;
                Debug.Log("Submitting Conversation to ChatGPT Npc");
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("Leaving chatGPT NPC.");
                doneEscape = true;
            }
            yield return null;
        }
    }

    private IEnumerator Conversation(ConversationArray conversationArray) 
    {
        isInConversation = true;
        TextBox.SetActive(true);

        foreach (var convo in conversationArray.conversations)
        {
            TextBox.GetComponentInChildren<TextMeshProUGUI>().text = convo;
            yield return WaitForKeyPressed();
        }

        TextBox.SetActive(false);
        isInConversation = false;
        EndOfConversationServerRPC();
    }

    private IEnumerator WaitForKeyPressed() 
    {
        bool done = false;
        while (!done) 
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                done = true; 
            }
            yield return null;
        }
    }

    [ClientRpc(Delivery = RpcDelivery.Reliable)]
    private void ChatGPTLoadingScreenClientRPC()
    {
        isInConversation = true;
        loadingScreen.SetActive(true);
    }

    [ServerRpc(Delivery = RpcDelivery.Reliable)]
    private void SendUserNameToServerRPC(string username)
    {

    }

    [ServerRpc(Delivery = RpcDelivery.Reliable)]
    private void DeleteLastChatGPTMessageServerRPC()
    {

    }

    [ServerRpc(Delivery = RpcDelivery.Reliable)]
    private void SendTextToChatGPTNPCServerRPC(string text)
    {

    }

    [ServerRpc(Delivery = RpcDelivery.Reliable)]
    private void EndOfConversationServerRPC()
    {
        Debug.Log("End Of Conversation");
    }

}

public class ConversationArray : INetworkSerializable
{
    public string[] conversations;

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

