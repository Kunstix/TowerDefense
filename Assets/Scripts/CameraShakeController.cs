using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeController : MonoBehaviour
{
    public static CameraShakeController instance;

    public Transform cameraTransform;
    public float shakeAmount = 0.7F;
    public float decreaseFactor = 1.0F;
    public float shakeSaveDurationValue = 1.0F;

    private float shakeDuration = 0.0F;
    private Vector3 originalPosition;

    private void Awake()
    {
        instance = this;
        if(cameraTransform == null)
        {
            cameraTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    private void OnEnable()
    {
        originalPosition = cameraTransform.localPosition;
    }

    public void ShakeCamera()
    {
        Debug.Log("Shake!" + shakeDuration + shakeSaveDurationValue);
        shakeDuration = shakeSaveDurationValue;
        originalPosition = cameraTransform.localPosition;
    }

    private void Update()
    {
        if(shakeDuration > 0)
        {
            Debug.Log("Shake!");
            cameraTransform.localPosition = originalPosition + Random.insideUnitSphere * shakeAmount;
            shakeDuration -= Time.deltaTime * decreaseFactor;
        } else
        {
            shakeDuration = 0;
            cameraTransform.localPosition = originalPosition;
        }
    }
}
