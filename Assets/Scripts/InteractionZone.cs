using System;
using UnityEngine;

public class InteractionZone : MonoBehaviour
{
    private Mirror mirror;
    private Door door;

    private void Awake()
    {
        mirror = GetComponentInParent<Mirror>();
        door = GetComponentInParent<Door>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent<PlayerController>(out var playerController))
            return;

        if (mirror != null)
        {
            playerController.AddNearbyMirror(mirror);
        }

        if (door != null)
        {
            playerController.SetDoor(door);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.TryGetComponent<PlayerController>(out var playerController))
            return;

        if (mirror != null)
        {
            playerController.RemoveNearbyMirror(mirror);
        }

        if (door != null)
        {
            playerController.ClearDoor();
        }
    }
}
