using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    public UnityEvent onOpened;
    private DoorState doorState;
    public enum DoorState : int
    {
        Locked,
        Unlocked,
        Open,
        NumStates
    }

    [SerializeField] private Material[] doorMaterials = new Material[(int)DoorState.NumStates];
    private Renderer doorRenderer;

    private void Awake()
    {
        doorRenderer = GetComponent<Renderer>();
        UpdateDoorState(DoorState.Locked);
    }

    public void Unlock()
    {
        if (doorState == DoorState.Locked)
        {
            UpdateDoorState(DoorState.Unlocked);
        }
    }

    public void Lock()
    {
        if (doorState == DoorState.Unlocked)
        {
            UpdateDoorState(DoorState.Locked);
        }
    }

    public void TryOpen()
    {
        if (doorState != DoorState.Unlocked)
        {
            return;
        }
        
        UpdateDoorState(DoorState.Open);
        onOpened.Invoke();
    }

    private void UpdateDoorState(DoorState state)
    { 
        doorState = state; 
        doorRenderer.material = doorMaterials[(int)state];
    }
}
