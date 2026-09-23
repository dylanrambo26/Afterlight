using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{ 
    [SerializeField] private float moveSpeed = 5f;
    private PlayerInput inputActions;
    private Vector2 moveInput;
    private Rigidbody2D rb;

    private Door currentDoor;
    
    [SerializeField]
    private Mirror currentMirror;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputActions = new PlayerInput();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.RotateLeft.performed += OnRotateLeft;
        inputActions.Player.RotateRight.performed += OnRotateRight;
        inputActions.Player.Reset.performed += OnReset;
        inputActions.Player.Interact.performed += OnInteract;
    }

    private void OnDisable()
    {
        inputActions.Player.RotateLeft.performed -= OnRotateLeft;
        inputActions.Player.RotateRight.performed -= OnRotateRight;
        inputActions.Player.Reset.performed -= OnReset;
        inputActions.Player.Interact.performed -= OnInteract;
        inputActions.Player.Disable();
    }

    private void Update()
    {
        moveInput = inputActions.Player.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }

    public void SetCurrentMirror(Mirror mirror)
    {
        currentMirror = mirror;
        
    }

    public void ClearCurrentMirror(Mirror mirror)
    {
        if (currentMirror == mirror)
        {
            currentMirror = null;
        }
    }

    public void SetDoor(Door door)
    {
        currentDoor = door;
    }

    public void ClearDoor()
    {
        currentDoor = null;
    }

    private void OnRotateLeft(InputAction.CallbackContext context)
    {
        if (currentMirror != null)
        {
            currentMirror.RotateLeft();
        }
    }
    
    private void OnRotateRight(InputAction.CallbackContext context)
    {
        if (currentMirror != null)
        {
            currentMirror.RotateRight();
        }
    }

    private void OnReset(InputAction.CallbackContext context)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetCurrentLevel();
        }
    }
    
    private void OnInteract(InputAction.CallbackContext context)
    {
        if (currentDoor != null)
        {
            currentDoor.TryOpen();
        }
    }
    
}
