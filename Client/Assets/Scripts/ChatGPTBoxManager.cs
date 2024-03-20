using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ChatGPTBoxManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ChatGPTTextBox;
    [SerializeField] TMP_InputField inputField;

    public void SetActive() 
    {
        ResetInputField();
        SetChatGPTTextBox("");
        this.gameObject.SetActive(true);
    } 

    public void SetChatGPTTextBox(string text) 
    {
        ChatGPTTextBox.text = text;
    }

    private void ResetInputField() 
    {
        inputField.text = "";
    }

    public TMP_InputField ReturnInputField => inputField;
}
