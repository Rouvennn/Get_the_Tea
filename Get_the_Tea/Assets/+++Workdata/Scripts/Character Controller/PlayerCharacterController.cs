using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    [Space]
    [Header("New Input System Values")]
    public InputActions inputActions;

    [Space]
    [Header("Character Movement")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float baseMovementSpeed = 5f;
    [SerializeField] private float currentMovementSpeed = 5f;
    [SerializeField] private float turnSpeed = 500f;
    [SerializeField] private float sprintSpeed = 1f;
    [SerializeField] private bool isSprinting;
    private Vector3 moveInput;

    //[Space]
    //[Header("CameraAngle")]
    //[SerializeField] CinemachineVirtualCamera cameraRotationOOC;


    RaycastHit hit;

    #region Unity Functions

    private void Awake()
    {
        inputActions = new InputActions();

        inputActions.Inputs.PlayerSprint.performed += ctx => Sprint();
        inputActions.Inputs.PlayerSprint.canceled += ctx => Sprint();
        //inputActions.CharacterInputs.Select.performed += ctx => Select();
        //inputActions.Inputs.PlayerInteract.performed += ctx => Interact();

        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        inputActions.Inputs.Enable();
    }

    private void OnDisable()
    {
        inputActions.Inputs.Disable();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    #endregion

    #region Basic Movement
    /// <summary>
    /// WASD Movement Calculations
    /// Camera Offset Calculations
    /// </summary>
    private void Movement()
    {
        moveInput = inputActions.Inputs.PlayerMovement.ReadValue<Vector3>();
        moveInput.Normalize();
        Vector3 rawMoveInput = moveInput * currentMovementSpeed + new Vector3(0.0f, 0.0f, 0.0f);

        var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
        var skewedInput = matrix.MultiplyPoint3x4(rawMoveInput);

        rb.linearVelocity = skewedInput;

        if (moveInput != Vector3.zero)
        {
            var relative = (transform.position + skewedInput) - transform.position;
            var rotation = Quaternion.LookRotation(relative, Vector2.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, turnSpeed * Time.deltaTime);
        }
    }


    public void Interact()
    {

    }

    /// <summary>
    /// Sprinting Calculations
    /// </summary>
    private void Sprint()
    {
        if (!isSprinting)
        {
            currentMovementSpeed = sprintSpeed;
        }
        else if (isSprinting)
        {
            currentMovementSpeed = baseMovementSpeed;
        }

        isSprinting = !isSprinting;
    }
    #endregion


//#if UNITY_EDITOR

//    void OnDrawGizmos()
//    {
//        Ray ray = new(transform.position, Vector3.down);
//        Gizmos.color = Color.green;
//        Gizmos.DrawLine(ray.origin, hit.point);

//        // Draw a sphere at the hit point 
//        Gizmos.color = Color.green;
//        Gizmos.DrawWireSphere(hit.point, 0.1f);
//    }

//#endif
}
