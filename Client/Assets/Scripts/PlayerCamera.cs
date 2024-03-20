using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class PlayerCamera : NetworkBehaviour
{
    [SerializeField] GameObject head;
    [SerializeField] GameObject playerBody;

    [SerializeField] int sensitivityVertical;
    [SerializeField] int sensitivityHorizontal;


    private Vector2 playerInput;
    private float verticalRotation;
    private float horizontalRotation;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerInput = Vector2.zero;
    }

    private void Start()
    {
        verticalRotation = head.transform.localEulerAngles.x;
        horizontalRotation = playerBody.transform.eulerAngles.y;
    }

    private void Update()
    {
        if(PlayerInteraction.isInConversation) return;
        if (Input.GetKeyDown(KeyCode.Escape))
            ToggleCursorMode();
        
        if (Cursor.lockState == CursorLockMode.Locked)
            Look();
    }

    private void Look() 
    {
        verticalRotation += -playerInput.y * sensitivityVertical * 0.01f;
        horizontalRotation += playerInput.x * sensitivityHorizontal * 0.01f;

        verticalRotation = Mathf.Clamp(verticalRotation, -85f, 85f);

        head.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        playerBody.transform.rotation = Quaternion.Euler(0f, horizontalRotation, 0f);

        SendRotationToServerRPC(new Vector3(verticalRotation, horizontalRotation, 0f), NetworkManager.LocalClientId);
       
    }

    private void ToggleCursorMode()
    {
        Cursor.visible = !Cursor.visible;

        if (Cursor.lockState == CursorLockMode.None)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;
    }

    public void LookAround(InputAction.CallbackContext callbackContext)
    {
        if (!IsOwner) return;
        playerInput = callbackContext.ReadValue<Vector2>();
    }

    [ServerRpc(Delivery = RpcDelivery.Unreliable)]
    private void SendRotationToServerRPC(Vector3 eulerRotationValues, ulong clientID)
    {
       
    }


    //Will send to the host as well
    [ClientRpc(Delivery = RpcDelivery.Unreliable)]
    private void SendRotationToClientRPC(Vector3 eulerRotationValues, ulong clientID) 
    {
        if (NetworkManager.Singleton.LocalClientId == clientID) return;
        head.transform.localRotation = Quaternion.Euler(eulerRotationValues.x, 0f, 0f);
        playerBody.transform.rotation = Quaternion.Euler(0f, eulerRotationValues.y, 0f);
    }
}
