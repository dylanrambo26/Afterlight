using System;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private Button mainMenuStartButton;
    
    private void Start()
    {
        mainMenuStartButton.onClick.AddListener(() => GameManager.Instance.LoadLevel(1));
    }
}
