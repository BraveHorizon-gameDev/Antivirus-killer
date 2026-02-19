using UnityEngine;

public class CameraOffset : MonoBehaviour
{
    public float yAxisIncrement = 5f;

    void Start()
    {
        Vector3 pos = transform.localPosition;
        pos.y = yAxisIncrement;
        transform.localPosition = pos;
    }
}