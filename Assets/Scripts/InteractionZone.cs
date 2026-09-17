using System;
using UnityEngine;

public class InteractionZone : MonoBehaviour
{
    private Mirror mirror;

    private void Awake()
    {
        mirror = GetComponentInParent<Mirror>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerController>(out var playerController))
        {
            playerController.SetCurrentMirror(mirror);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerController>(out var playerController))
        {
            playerController.ClearCurrentMirror(mirror);
        }
    }
}
