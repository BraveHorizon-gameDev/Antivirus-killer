using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public float mouseSensitivity = 600f;
    public Transform playerBody;
    public float minPitch = -89f;
    public float maxPitch = 89f;
    private float pitch = 0f;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
    }

    // Update is called once per frame
    void Update()
    {
        // Mouse look input
        float mouseX = Input.GetAxis(axisName: "Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis(axisName: "Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Adjust pitch and clamp it
        pitch -= mouseY;
        pitch = Mathf.Clamp(value: pitch, min: minPitch, max: maxPitch);

        // Apply rotations
        transform.localRotation = Quaternion.Euler(x: pitch, y: 0f, z: 0f);
        playerBody.Rotate(eulers: Vector3.up * mouseX);
    }
}