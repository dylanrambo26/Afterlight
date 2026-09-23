using System;
using UnityEngine;
using UnityEngine.Events;

public class LaserReceiver : MonoBehaviour
{
    public UnityEvent onStateChanged;
    private Door door;
    
    public bool IsActivated { get; private set; }

    private void Awake()
    {
        door = GameObject.FindGameObjectWithTag("Door").GetComponent<Door>();
    }

    public void Activate()
    {
        if (IsActivated)
        {
            return;
        }
        IsActivated = true;
        onStateChanged.Invoke();
    }

    public void Deactivate()
    {
        if (!IsActivated)
        {
            return;
        }
        
        IsActivated = false;
        onStateChanged.Invoke();
    }
}
