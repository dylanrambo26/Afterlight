using System;
using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    [SerializeField] private GameManager gameManagerPrefab;

    private void Awake()
    {
        if (GameManager.Instance == null)
        {
            Instantiate(gameManagerPrefab);
        }
    }
}
