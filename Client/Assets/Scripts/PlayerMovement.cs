using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;
using TMPro;
using System;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : NetworkBehaviour
{
    NetworkVariable<Vector3> inputDirection = new NetworkVariable<Vector3>(Vector3.zero, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    NetworkVariable<bool> isRunning = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    NetworkVariable<int> timer = new NetworkVariable<int>(300, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    [SerializeField] TextMeshProUGUI timerObject;

    private void Awake()
    {
        timer.OnValueChanged += ChangeTimer;
    }

    private void ChangeTimer(int previousValue, int newValue)
    {
        timerObject.text = "Seconds Left: " + newValue.ToString();
        Debug.Log(previousValue);
        Debug.Log(newValue);
    }

    public void Move(InputAction.CallbackContext callbackContext)
    {
        if (!IsOwner || PlayerInteraction.isInConversation) return;
        inputDirection.Value = callbackContext.ReadValue<Vector3>();
        if (!(inputDirection.Value.z > 0) && !(callbackContext.control.device is Keyboard)) isRunning.Value = false;
    }

    public void Jump(InputAction.CallbackContext callbackContext)
    {
        if (!IsOwner || PlayerInteraction.isInConversation) return;
        if (callbackContext.started)
        { 
            SendJumpToServerRPC();
        }

    }

    [ServerRpc(Delivery = RpcDelivery.Unreliable)]
    private void SendJumpToServerRPC()
    {
        
    }

    public void Run(InputAction.CallbackContext callbackContext)
    {
        if (!IsOwner) return;
        if (callbackContext.started) isRunning.Value = true;
        else if (callbackContext.canceled && callbackContext.control.device is Keyboard) isRunning.Value = false;
    }



}
