using UnityEngine;
using UnityEngine.XR;

public class RotateXROrigin : MonoBehaviour
{
    public float rotationSpeed = 10.0f;

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 rotation = new Vector3(verticalInput, horizontalInput, 0.0f) * rotationSpeed * Time.deltaTime;
        transform.Rotate(rotation, Space.Self);
    }
}

