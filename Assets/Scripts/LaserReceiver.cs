using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LaserReceiver : MonoBehaviour
{
    public UnityEvent onStateChanged;
    
    public GameObject lensGameObject;

    private HashSet<LaserEmitter> activeEmitters = new();
    
    [SerializeField] private PowerCable powerCable;
    
    public bool IsActivated { get; private set; }

    public void Activate(LaserEmitter emitter)
    {
        activeEmitters.Add(emitter);
        
        if (IsActivated)
        {
            return;
        }
        
        IsActivated = true;
        powerCable.SetPowered(true);
        onStateChanged.Invoke();
        lensGameObject.SetActive(true);
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
        powerCable.SetPowered(false);
        onStateChanged.Invoke();
        lensGameObject.SetActive(false);
    }
}
