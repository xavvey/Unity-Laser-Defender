using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float shakeDuration = 0.5f;
    [SerializeField] float shakeMagnitude = 0.25f;
    Vector3 initCamPosition;

    void Start()
    {
        initCamPosition = transform.position;
    }

    public void Play()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        float ellapsedTime = 0f;
        while (ellapsedTime < shakeDuration)
        {
            transform.position = initCamPosition + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            ellapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = initCamPosition;
    }
}
