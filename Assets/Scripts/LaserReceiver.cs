using System;
using UnityEngine;
using UnityEngine.Events;

public class LaserReceiver : MonoBehaviour
{
    public UnityEvent onStateChanged;
    private Door door;

    public Material receiverOff;
    public Material receiverOn;
    
    private Renderer receiverRenderer;
    
    public bool IsActivated { get; private set; }

    private void Awake()
    {
        door = GameObject.FindGameObjectWithTag("Door").GetComponent<Door>();
        receiverRenderer = GetComponent<Renderer>();
    }

    public void Activate()
    {
        if (IsActivated)
        {
            return;
        }
        IsActivated = true;
        onStateChanged.Invoke();
        receiverRenderer.material = receiverOn;
    }

    public void Deactivate()
    {
        if (!IsActivated)
        {
            return;
        }
        
        IsActivated = false;
        onStateChanged.Invoke();
        receiverRenderer.material = receiverOff;
    }
}
