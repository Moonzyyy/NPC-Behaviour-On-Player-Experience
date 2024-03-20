using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;


[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] CharacterController characterController;

    NetworkVariable<Vector3> inputDirection = new NetworkVariable<Vector3>(Vector3.zero, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    Vector3 storedDirection;
    float gravity;

    [SerializeField] float assignedMovementSpeedValue;
    float movementSpeed;

    bool isJumping;
    [SerializeField] float assignedJumpForce;
    float jumpForce;
    bool hasStoredInitialAirDirection;

    NetworkVariable<bool> isRunning = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    bool hasRanBeforeJumping;

    float yVelocity;

    NetworkVariable<int> timer = new NetworkVariable<int>(300, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    private void OnValidate()
    {
        Initialize();
    }

    private void Awake()
    {
        Initialize();
    }


    private void Initialize()
    {
        if (characterController == null) characterController = GetComponent<CharacterController>();
        storedDirection = Vector3.zero;
        movementSpeed = assignedMovementSpeedValue * Time.fixedDeltaTime;
        gravity = -9.81f * Time.fixedDeltaTime * Time.fixedDeltaTime;
        jumpForce = Mathf.Sqrt(assignedJumpForce * -2f * gravity);
        hasStoredInitialAirDirection = false;
        hasRanBeforeJumping = false;
        isJumping = false;
    }

    private void Update()
    {
        timer.Value = (int)NetworkManagerHelper.actualTimer;
    }

    //could probably clean this code
    private void FixedUpdate()
    {
        if (NetworkManager.Singleton.IsClient) return;

        Vector3 moveDirection;
        //Logic of what happends on the ground
        if (characterController.isGrounded)
        {
            yVelocity = 0f;
            hasStoredInitialAirDirection = false;
            hasRanBeforeJumping = false;
            if (isJumping)
            {
                yVelocity = jumpForce;
                isJumping = false;
                storedDirection = inputDirection.Value;
                hasStoredInitialAirDirection = true;
                hasRanBeforeJumping = isRunning.Value;
            }

            moveDirection = Vector3.Normalize(transform.right * inputDirection.Value.x + Vector3.Normalize(transform.forward) * inputDirection.Value.z);
            moveDirection *= movementSpeed;
            if (isRunning.Value && inputDirection.Value.z > 0) moveDirection *= 1.5f;
            yVelocity += gravity;
            moveDirection.y = yVelocity;

        }
        //Logic of what happens in the air
        else
        {
            if (!hasStoredInitialAirDirection)
            {
                storedDirection = inputDirection.Value;
                hasStoredInitialAirDirection = true;
            }
            moveDirection = Vector3.Normalize(transform.right * storedDirection.x + Vector3.Normalize((transform.forward)) * storedDirection.z);
            moveDirection *= movementSpeed;
            if (hasRanBeforeJumping) moveDirection *= 1.5f;
            yVelocity += gravity;
            moveDirection.y = yVelocity;

        }
        characterController.Move(moveDirection);

    }

    [ServerRpc(Delivery = RpcDelivery.Unreliable)]
    private void SendJumpToServerRPC()
    {
        if (characterController.isGrounded) isJumping = true;
    }


}
