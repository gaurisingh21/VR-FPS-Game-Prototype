using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadsetLook : MonoBehaviour
{
    public Transform cameraTransform;
    public float sensitivty = 400f;
    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Input.gyro.enabled = true;
        Input.gyro.updateInterval = 0.0167f; // 60 Hz
        cameraTransform = transform.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.gyro.rotationRateUnbiased.y * sensitivty * Time.deltaTime;
        float mouseY = Input.gyro.rotationRateUnbiased.x * sensitivty * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

}
