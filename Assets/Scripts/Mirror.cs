using System.Collections;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    private float rotateSpeed = 90f;
    private Quaternion originalRotation;
    private Quaternion targetRotation;
    private float rotationAmount = 45f;


    private int rotationStep = 0;
    private bool isRotating = false;
    
    private void Awake()
    {
        originalRotation = transform.rotation;
        targetRotation = originalRotation;
    }

    public void Rotate()
    {
        if (isRotating)
        {
            return;
        }

        rotationStep = (rotationStep + 1) % 8;

        targetRotation = originalRotation * Quaternion.Euler(0f, 0f, rotationStep * rotationAmount);
        StartCoroutine(RotateMirror());
    }

    private IEnumerator RotateMirror()
    {
        isRotating = true;
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotateSpeed * Time.deltaTime
            );

            yield return null;
        }

        transform.rotation = targetRotation;
        isRotating = false;
    }
}
