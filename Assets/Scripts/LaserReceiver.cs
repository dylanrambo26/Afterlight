using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LaserReceiver : MonoBehaviour
{
    public UnityEvent onStateChanged;

    public Material receiverOff;
    public Material receiverOn;
    
    private Renderer receiverRenderer;

    private HashSet<LaserEmitter> activeEmitters = new();
    
    public bool IsActivated { get; private set; }

    private void Awake()
    {
        receiverRenderer = GetComponent<Renderer>();
    }

    public void Activate(LaserEmitter emitter)
    {
        activeEmitters.Add(emitter);
        
        if (IsActivated)
        {
            return;
        }
        
        IsActivated = true;
        onStateChanged.Invoke();
        receiverRenderer.material = receiverOn;
    }

    public void Deactivate(LaserEmitter emitter)
    {
        activeEmitters.Remove(emitter);
        
        //Make sure no emitters are hitting before deactivating
        if (activeEmitters.Count > 0)
        {
            return;
        }
        
        if (!IsActivated)
        {
            return;
        }
        
        IsActivated = false;
        onStateChanged.Invoke();
        receiverRenderer.material = receiverOff;
    }
}
