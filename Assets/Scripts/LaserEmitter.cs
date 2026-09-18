using System.Collections.Generic;
using UnityEngine;

public class LaserEmitter : MonoBehaviour
{
    [SerializeField] private float maxDistance = 25f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private LineRenderer lineRenderer;
    private int maxReflections = 10;

    // Update is called once per frame
    private void FixedUpdate()
    {
        CalculateLaserPath();
    }

    private void CalculateLaserPath()
    {
        List<Vector3> points = new List<Vector3>();
        
        Vector2 origin = transform.position;
        Vector2 direction = Vector2.right;
        
        points.Add(origin);

        for (int i = 0; i < maxReflections; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(
                origin, direction,
                maxDistance,
                layerMask
            );
            
            if (hit.collider ==null)
            {
                points.Add(origin + direction * maxDistance);
                break;
            }
            points.Add(hit.point);

            if (hit.collider.TryGetComponent<LaserReceiver>(out var receiver))
            {
                receiver.Activate();
                print("hit receiver");
                break;
            }

            if (hit.collider.TryGetComponent<PlayerController>(out _))
            {
                GameManager.Instance?.ResetCurrentLevel();
                break;
            }
            
            if (!hit.collider.CompareTag("Mirror"))
            {
                break;
            }
            direction = Vector2.Reflect(direction, hit.normal);

            origin = hit.point + direction * 0.01f;
        }

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }
}
