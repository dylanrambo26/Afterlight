using System;
using System.Collections.Generic;
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

    private readonly HashSet<Mirror> nearbyMirrors = new();

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

    public void AddNearbyMirror(Mirror mirror)
    {
        if(mirror != null)
        {
            nearbyMirrors.Add(mirror);
        }
    }

    public void RemoveNearbyMirror(Mirror mirror)
    {
        nearbyMirrors.Remove(mirror);
    }

    private Mirror GetNearbyMirror()
    {
        Mirror closestMirror = null;
        float closestDistance = float.MaxValue;

        foreach (Mirror mirror in nearbyMirrors)
        {
            if (mirror == null)
            {
                continue;
            }
            
            float distance = (mirror.transform.position - transform.position).sqrMagnitude;

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestMirror = mirror;
                closestMirror.isClosestMirror = true;
            }
            else
            {
                closestMirror.isClosestMirror = false;
            }
        }
        
        return closestMirror;
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
        Mirror mirror = GetNearbyMirror();
        if (mirror != null)
        {
            mirror.RotateLeft();
        }
    }
    
    private void OnRotateRight(InputAction.CallbackContext context)
    {
        Mirror mirror = GetNearbyMirror();
        if (mirror != null)
        {
            mirror.RotateRight();
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
