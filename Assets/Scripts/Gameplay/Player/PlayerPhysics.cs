using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerPhysics : PlayerStatus {

    [Header("Player Physics")]
    [SerializeField] private float groundRadius = .02f;
    [SerializeField] private float mass = 1.5f;
    [SerializeField] private float jumpHeight = 10f;
    [SerializeField] private float aimSpeed = 2f;
    [SerializeField] private float walkSpeed = 7f;
    [SerializeField] private float sprintSpeed = 12f;

    [Header(" - Config")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;

    private Vector2 velocity;
    public bool isGrounded { get; private set; }
    public bool isJumping;
    private const float gravity = -9.81f;

    private void FixedUpdate() {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, whatIsGround);
    }

    public void UpdatePhysics(Vector2 move, float lookX, bool sprint, bool aim) {

        // Move
        Vector3 position = (transform.right * move.x) + (transform.forward * move.y);
        float moveSpeed = aim ? aimSpeed : sprint ? sprintSpeed : walkSpeed;
        characterController.Move(position * moveSpeed * Time.fixedDeltaTime);

        // Gravity and Jump Velocity
        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }
        velocity.y += gravity * mass * Time.fixedDeltaTime;
        characterController.Move(velocity * Time.fixedDeltaTime);

        // Rotação do player
        transform.Rotate(Vector3.up * lookX);
    }
    public bool CanJump() {
        if (isGrounded && !isJumping) {
            return true;
        }
        return false;
    }

    public void PlayerDeath() {
        characterController.enabled = false;
    }

    public void OnJump() {
        print("Jump");
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity * mass);
    }
    public void OnLanding() {
        print("Land");
        isJumping = false;
    }

}
