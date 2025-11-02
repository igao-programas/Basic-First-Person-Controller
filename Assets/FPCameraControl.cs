using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class FPCameraControl : MonoBehaviour
{
    // Attributes
    public Transform playerBody;
    private float _verticalTilt = 0f;
    
    // Player Input
    public InputActionReference cameraMovementInput;
    private InputDevice lastDevice;
    
    [SerializeField, Range(0, 3)] private float mouseSensitivity = 0.2f;
    [SerializeField, Range(0, 10)] private float gamepadSensitivity = 2.2f;
    
    
    void Start() {
        Application.targetFrameRate = 60;
        Cursor.lockState = CursorLockMode.Locked;

        cameraMovementInput.action.performed += OnMove; // Adds listener for movement
    }
    
    private void OnMove(InputAction.CallbackContext context)
    {
        lastDevice = context.control.device; // Registers Last Device used by Player
    }
    
    void Update() {
        // Camera Input
        Vector2 rawCameraInput = cameraMovementInput.action.ReadValue<Vector2>();
        float sensivity = (lastDevice is Gamepad) ? gamepadSensitivity : mouseSensitivity;
        float mouseX = rawCameraInput.x * sensivity;
        float mouseY = rawCameraInput.y * sensivity;
        
        // Camera Rotation
        float horizontalTilt = mouseX;
        _verticalTilt -= mouseY;
        _verticalTilt = Mathf.Clamp(_verticalTilt, -90f, 90f);
        
        // Apply Rotations
        transform.localRotation = Quaternion.Euler(_verticalTilt, 0f, 0f);
        playerBody.Rotate(Vector3.up * horizontalTilt);

    }
}
