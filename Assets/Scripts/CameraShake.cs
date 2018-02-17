using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    [SerializeField]
    private bool debugMode = true;
    [SerializeField]
    private float shakeAmount;
    [SerializeField]
    private float shakeDuration;
    [SerializeField]
    private bool smooth;
    [SerializeField]
    private float smoothAmount = 5f;

    private float startDuration;
    private float startAmount;
    private float shakePercentage;

    private bool isRunning = false;

    private void OnEnable()
    {
        EventManager.OnGameOver += ShakeCamera;
    }


    private void Start()
    {
        if (debugMode)
        {
            ShakeCamera();
        }
    }

    private void OnDisable()
    {
        EventManager.OnGameOver -= ShakeCamera;
    }

    void ShakeCamera()
    {
        //startAmount = shakeAmount;
        //startDuration = shakeDuration;

        //if (!isRunning) StartCoroutine(Shake());

        ShakeCamera(8, 0.005f);
    }

    public void ShakeCamera(float amount, float duration)
    {
        shakeAmount += amount;
        startAmount = shakeAmount;
        shakeDuration += duration;
        startDuration = shakeDuration;

        if (!isRunning) StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        var initialShakeDuration = shakeDuration;
        var initialShakeAmount = shakeAmount;

        while (shakeDuration > 0.01f)
        {
            Vector3 rotationAmount = Random.insideUnitSphere * shakeAmount;
            rotationAmount.z = 0;

            shakePercentage = startAmount * shakePercentage;
            shakeDuration = Mathf.Lerp(shakeDuration, 0, Time.deltaTime);

            if (smooth)
            {
                transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(rotationAmount), Time.deltaTime * smoothAmount);
            }
            else
            {
                transform.localRotation = Quaternion.Euler(rotationAmount);
            }

            yield return null;
        }

        transform.localRotation = Quaternion.identity;
        isRunning = false;
        shakeAmount = initialShakeAmount;
        shakeDuration = initialShakeDuration;
    }
}