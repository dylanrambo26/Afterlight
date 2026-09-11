using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private LaserReceiver[] requiredReceivers;
    
    void Start()
    {
        foreach (var receiver in requiredReceivers)
        {
            receiver.onActivated.AddListener(CheckLevelComplete);
        }
    }

    private void CheckLevelComplete()
    {
        foreach (var receiver in requiredReceivers)
        {
            if (!receiver.IsActivated)
            {
                return;
            }
            
            GameManager.Instance.GoToNextLevel();
        }
    }
}
