using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.1f;
    public float shakeStrength = 0.1f;

    private Vector3 baseLocalPos;
    private float timer;

    void Start()
    {
        baseLocalPos = transform.localPosition;
    }

    void LateUpdate()
    {
        if (timer > 0f)
        {
            transform.localPosition = baseLocalPos +
                Random.insideUnitSphere * shakeStrength;

            timer -= Time.deltaTime;
        }
        else
        {
            transform.localPosition = baseLocalPos;
        }
    }

    public void Shake()
    {
        baseLocalPos = transform.localPosition;
        timer = shakeDuration;
    }
}
