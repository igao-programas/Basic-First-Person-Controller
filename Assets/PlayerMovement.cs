using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public CharacterController controller;
    private Vector3 velocity;
    private Vector3 acceleration;
    
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;
    
    [Range(0, 100)] public float _playerSpeed = 25f;
    [Range(0, 50)] public float _gravity = 9.8f;
    [Range(0, 10)] public float _jumpHeight = 3f;
    
    void Start() {
        acceleration = new Vector3(0, -_gravity, 0);
    }
    
    void Update() {
        // Input Handling
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");
        Vector3 movementInput = (transform.right * xInput + transform.forward * zInput) * _playerSpeed;
        
        // Physics Handling
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        velocity += acceleration * Time.deltaTime;

        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }
        
        if (Input.GetButtonDown("Jump") && isGrounded) {
            velocity.y = Mathf.Sqrt(_jumpHeight * -2f * acceleration.y);
        }
        
        Vector3 totalMovement = (velocity + movementInput) * Time.deltaTime;
        controller.Move(totalMovement);
    }
}
