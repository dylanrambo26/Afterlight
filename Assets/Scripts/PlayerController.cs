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
    private Mirror closestMirror;

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
        if (closestMirror != null)
        {
            closestMirror.SetSelected(false);
        }
        closestMirror = null;
        
        inputActions.Player.RotateLeft.performed -= OnRotateLeft;
        inputActions.Player.RotateRight.performed -= OnRotateRight;
        inputActions.Player.Reset.performed -= OnReset;
        inputActions.Player.Interact.performed -= OnInteract;
        inputActions.Player.Disable();
    }

    private void Update()
    {
        moveInput = inputActions.Player.Move.ReadValue<Vector2>();
        UpdateClosestMirror();
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

    private void UpdateClosestMirror()
    {
        Mirror newClosestMirror = null;
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
                newClosestMirror = mirror;
            }
        }

        if (newClosestMirror == closestMirror)
            return;

        closestMirror?.SetSelected(false);

        closestMirror = newClosestMirror;
        closestMirror?.SetSelected(true);
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
        if (closestMirror != null)
        {
            closestMirror.RotateLeft();
        }
    }
    
    private void OnRotateRight(InputAction.CallbackContext context)
    {
        if (closestMirror != null)
        {
            closestMirror.RotateRight();
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
