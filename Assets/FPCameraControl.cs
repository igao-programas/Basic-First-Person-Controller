using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPCameraControl : MonoBehaviour
{
    public Transform playerBody;
    private float _verticalTilt = 0f;
    
    [Range(0, 3)] public float mouseSensivity = 4f;
    
    void Start() {
        Application.targetFrameRate = 60;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensivity;
        
        float horizontalTilt = mouseX;
        _verticalTilt -= mouseY;
        _verticalTilt = Mathf.Clamp(_verticalTilt, -90f, 90f);
        
        transform.localRotation = Quaternion.Euler(_verticalTilt, 0f, 0f);
        playerBody.Rotate(Vector3.up * horizontalTilt);
    }
}
