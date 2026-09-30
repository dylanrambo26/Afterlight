using System;
using System.Collections;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    private float rotateSpeed = 90f;
    private Quaternion originalRotation;
    private Quaternion targetRotation;
    
    public bool isClosestMirror = false;

    private Renderer mirrorRenderer;
    
    [SerializeField]
    private Material selectedMaterial;
    
    [SerializeField]
    private Material unselectedMaterial;
    
    private int rotationStep;
    private bool isRotating;

    public void SetSelected(bool selected)
    {
        mirrorRenderer.sharedMaterial = selected ? selectedMaterial : unselectedMaterial;
    }

    private void Awake()
    {
        originalRotation = transform.rotation;
        targetRotation = originalRotation;
        mirrorRenderer = GetComponent<Renderer>();
        
        SetSelected(false);
    }

    public void RotateLeft()
    {
        Rotate(-45f);
    }

    public void RotateRight()
    {
        Rotate(45f);
    }
    
    private void Rotate(float angle)
    {
        if (isRotating)
        {
            return;
        }
        
        targetRotation = transform.rotation * Quaternion.Euler(0f, 0f, angle);
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
