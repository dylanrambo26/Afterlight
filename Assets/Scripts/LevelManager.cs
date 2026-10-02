using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private LaserReceiver[] requiredReceivers;

    private Door door;
    
    void Start()
    {
        foreach (LaserReceiver receiver in requiredReceivers)
        {
            receiver.onStateChanged.AddListener(CheckReceivers);
        }
        
        door = GameObject.FindGameObjectWithTag("Door").GetComponent<Door>();
        door.onOpened.AddListener(CompleteLevel);
        
        CheckReceivers();
    }

    private void CheckReceivers()
    {
        if (requiredReceivers == null || requiredReceivers.Length == 0)
        {
            Debug.LogError($"No required receivers assigned in {gameObject.name}", this);
        }
        
        foreach (var receiver in requiredReceivers)
        {
            if (!receiver.IsActivated)
            {
                door.Lock();
                return;
            }
        }
        
        door.Unlock();
    }

    private void CompleteLevel()
    {
        GameManager.Instance.GoToNextLevel();
    }
}
