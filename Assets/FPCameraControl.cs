using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPCameraControl : MonoBehaviour
{
    public Transform playerBody;
    private float _verticalTilt = 0f;
    public InputActionReference cameraMovement;
    
    [Range(0, 3)] public float mouseSensitivity = 1f;
    [Range(0, 10)] public float gamepadSensitivity = 4f;
    public bool gamepadOn = false;
    
    void Start() {
        Application.targetFrameRate = 60;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    
    
    void Update() {
        Vector2 cameraInput = cameraMovement.action.ReadValue<Vector2>();
        
        float sensivity = mouseSensitivity;
        if (gamepadOn) {sensivity = gamepadSensitivity;}
        
        float mouseX = cameraInput.x * sensivity;
        float mouseY = cameraInput.y * sensivity;
        
        float horizontalTilt = mouseX;
        _verticalTilt -= mouseY;
        _verticalTilt = Mathf.Clamp(_verticalTilt, -90f, 90f);
        
        transform.localRotation = Quaternion.Euler(_verticalTilt, 0f, 0f);
        playerBody.Rotate(Vector3.up * horizontalTilt);
    }
}
