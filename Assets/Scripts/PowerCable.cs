using System;
using System.Collections;
using UnityEngine;

public class PowerCable : MonoBehaviour
{
    [SerializeField] private LineRenderer powerLine;
    [SerializeField] private Transform[] pathPoints;
    [SerializeField] private float powerSpeed = 3f;

    private Coroutine powerCoroutine;
    private bool isPowered;

    [SerializeField] private float fadeDuration = 0.5f;
    private Coroutine fadeCoroutine;
    
    private void Awake()
    {
        powerLine.useWorldSpace = true;
        powerLine.enabled = false;
    }

    public void SetPowered(bool powered)
    {
        if (isPowered == powered)
            return;

        isPowered = powered;

        if (powerCoroutine != null)
        {
            StopCoroutine(powerCoroutine);
            powerCoroutine = null;
        }
        
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
            fadeCoroutine = null;
        }

        if (powered)
        {
            Color startColor = powerLine.startColor;
            Color endColor = powerLine.endColor;

            startColor.a = 1f;
            endColor.a = 1f;

            powerLine.startColor = startColor;
            powerLine.endColor = endColor;

            powerLine.enabled = true;

            powerCoroutine = StartCoroutine(AnimatePower());
        }
        else
        {
            fadeCoroutine = StartCoroutine(FadePower());
        }
    }

    private IEnumerator AnimatePower()
    {
        if (pathPoints.Length < 2)
            yield break;

        Vector3[] points = new Vector3[pathPoints.Length];

        for (int i = 0; i < pathPoints.Length; i++)
        {
            points[i] = pathPoints[i].position;
        }

        powerLine.positionCount = 1;
        powerLine.SetPosition(0, points[0]);

        for (int i = 0; i < points.Length - 1; i++)
        {
            Vector3 start = points[i];
            Vector3 end = points[i + 1];

            float segmentLength = Vector3.Distance(start, end);
            float distanceTraveled = 0f;

            powerLine.positionCount = i + 2;

            while (distanceTraveled < segmentLength)
            {
                distanceTraveled = Mathf.Min(
                    distanceTraveled + powerSpeed * Time.deltaTime,
                    segmentLength
                );

                Vector3 currentPosition = Vector3.Lerp(
                    start,
                    end,
                    distanceTraveled / segmentLength
                );

                powerLine.SetPosition(i + 1, currentPosition);

                yield return null;
            }

            powerLine.SetPosition(i + 1, end);
        }

        powerCoroutine = null;
    }
    
    private IEnumerator FadePower()
    {
        float elapsedTime = 0f;

        Color startColor = powerLine.startColor;
        Color endColor = powerLine.endColor;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            float t = Mathf.Clamp01(elapsedTime / fadeDuration);

            Color newStartColor = startColor;
            Color newEndColor = endColor;

            newStartColor.a = Mathf.Lerp(startColor.a, 0f, t);
            newEndColor.a = Mathf.Lerp(endColor.a, 0f, t);

            powerLine.startColor = newStartColor;
            powerLine.endColor = newEndColor;

            yield return null;
        }

        powerLine.enabled = false;
        powerLine.positionCount = 0;

        fadeCoroutine = null;
    }
}
