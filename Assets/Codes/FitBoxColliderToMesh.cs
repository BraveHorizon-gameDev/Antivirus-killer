using UnityEngine;

[ExecuteInEditMode]
public class FitBoxColliderToMesh : MonoBehaviour
{
    void Start()
    {
        BoxCollider box = GetComponent<BoxCollider>();
        MeshFilter mf = GetComponent<MeshFilter>();

        if (box == null || mf == null) return;

        box.center = mf.sharedMesh.bounds.center;
        box.size = mf.sharedMesh.bounds.size;
    }
}
