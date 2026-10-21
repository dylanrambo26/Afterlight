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
    [SerializeField] private Transform reflector;
    [SerializeField] private GameObject selectionIndicator;
    
    /*[SerializeField]
    private Material selectedMaterial;
    
    [SerializeField]
    private Material unselectedMaterial;*/
    
    private int rotationStep;
    public bool isRotating;

    public void SetSelected(bool selected)
    {
        selectionIndicator.SetActive(selected);
    }

    private void Awake()
    {
        originalRotation = reflector.rotation;
        targetRotation = originalRotation;
    }

    public void RotateLeft()
    {
        Rotate(45f);
    }

    public void RotateRight()
    {
        Rotate(-45f);
    }
    
    private void Rotate(float angle)
    {
        if (isRotating)
        {
            return;
        }
        
        targetRotation = reflector.localRotation * Quaternion.Euler(0f, 0f, angle);
        StartCoroutine(RotateMirror());
    }

    private IEnumerator RotateMirror()
    {
        isRotating = true;
        while (Quaternion.Angle(reflector.localRotation, targetRotation) > 0.1f)
        {
            reflector.localRotation = Quaternion.RotateTowards(
                reflector.localRotation,
                targetRotation,
                rotateSpeed * Time.deltaTime
            );

            yield return null;
        }

        reflector.localRotation = targetRotation;
        isRotating = false;
    }
}
