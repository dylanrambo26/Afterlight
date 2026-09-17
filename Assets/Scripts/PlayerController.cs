using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{ 
    [SerializeField] private float moveSpeed = 5f;
    private PlayerInput inputActions;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    
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
        inputActions.Player.Interact.performed += OnInteract;
    }

    private void OnDisable()
    {
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

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (currentMirror != null)
        {
            currentMirror.Rotate();
        }
    }
}
