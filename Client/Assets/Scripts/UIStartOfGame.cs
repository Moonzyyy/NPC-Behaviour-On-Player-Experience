using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.InputSystem;

public class UIStartOfGame : MonoBehaviour
{
    [SerializeField] Button joinButton;
    [SerializeField] TMP_InputField username;
    [SerializeField] TextMeshProUGUI disconnectText;
    public static string submittedUsername;
    private CanvasGroup canvasGroup;

    private static UIStartOfGame singleton;

    public static UIStartOfGame Singleton 
    {
        get { return singleton; }
        set {
            if (singleton != null)
            {
                Destroy(value.gameObject);
                Debug.Log("Destroying new setter of UIStartOfGame");
            }
            singleton = value; 
        }
    }

    private void Awake()
    {
        Singleton = this;
        canvasGroup = GetComponent<CanvasGroup>();
        SetVisibleAndInteractable();
        joinButton.onClick.AddListener(() => 
            {
                string time = PlayerPrefs.GetString("Time");
                double elapsed;
                if (!time.Equals(""))
                {
                    DateTime oldDateTime = DateTime.Parse(time);
                    TimeSpan elapsedTime = DateTime.Now - oldDateTime;
                    elapsed = elapsedTime.TotalSeconds;
                }
                else 
                {
                    elapsed = 700;
                }
                if (elapsed >= 600) 
                {
                    submittedUsername = username.text;
                    NetworkManager.Singleton.StartClient();
                }
                else
                {
                    UIStartOfGame.Singleton.SetDisconnectText($"You recently played the game and are now on timeout.\nYou still have {(int)(600 - elapsed)} seconds left!");
                }

            });
    }

    public void SetVisibleAndInteractable() 
    { 
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        username.ActivateInputField();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void SetInVisibleAndUnInteractable()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        username.DeactivateInputField();
    }

    Coroutine coroutine;
    public void SetDisconnectText(string connectionErrorMessage) 
    { 
        if(coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(DisplayErrorMessage(connectionErrorMessage));
    }

    IEnumerator DisplayErrorMessage(string connectionErrorMessage) 
    { 
        disconnectText.text = connectionErrorMessage;
        yield return new WaitForSeconds(5);
        disconnectText.text = "";
        coroutine = null;
        
    }
}
