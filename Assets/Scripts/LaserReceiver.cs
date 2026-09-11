using System;
using UnityEngine;
using UnityEngine.Events;

public class LaserReceiver : MonoBehaviour
{
    public UnityEvent onActivated;
    
    public bool IsActivated { get; private set; }

    public void Activate()
    {
        if (IsActivated)
        {
            return;
        }
        
        IsActivated = true;
        onActivated.Invoke();
        print("invoked");
    }

    public void Deactivate()
    {
        IsActivated = false;
    }
}
