using UnityEngine;

public class ArenaSpawner : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tag: "Virus"))
        {
            other.transform.position = GetSafePosition();
        }
    }

    Vector3 GetSafePosition()
    {
        return new Vector3(x: 0, y: 2, z: 0);
    }
}