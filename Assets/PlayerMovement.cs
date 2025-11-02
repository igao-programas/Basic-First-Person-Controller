using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
    
    // Attributes
    private Vector3 velocity;
    private Vector3 acceleration;
    [Range(0, 50)] public float _gravity = 9.8f;
    
    // Player Input
    public CharacterController controller;
    public InputActionReference move;
    public InputActionReference jump;
    
    [Range(0, 100)] public float _playerSpeed = 25f;
    [Range(0, 10)] public float _jumpHeight = 3f;
    
    // Ground Checking
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;
    
    
    void Start() {
        acceleration = new Vector3(0, -_gravity, 0);

    }
    
    
    void Update() {
        // Movement Input
        Vector3 rawMovementInput = this.move.action.ReadValue<Vector2>();
        Vector3 movementLocalVector = new Vector3(rawMovementInput.x, 0, rawMovementInput.y);
        Vector3 movementFromInput = transform.TransformDirection(movementLocalVector) * _playerSpeed;
        
        // Physics Handling
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        velocity += acceleration * Time.deltaTime;

        // Ground Clamping
        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }
        
        // Jump Action
        if (jump.action.triggered && isGrounded) {
            velocity.y = Mathf.Sqrt(_jumpHeight * -2f * acceleration.y);
        }
        
        // Apply Movement
        Vector3 totalMovement = (velocity + movementFromInput) * Time.deltaTime;
        controller.Move(totalMovement);
    }
}
