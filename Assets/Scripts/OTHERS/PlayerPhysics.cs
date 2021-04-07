using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerPhysics : MonoBehaviour {

    [Header("Player Physics")]
    [SerializeField] private float groundRadius = .02f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 10f;
    [SerializeField] private float moveSpeed = 10f;

    [Header(" - Config")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;

    private Vector2 velocity;
    private bool isGrounded;

    private void FixedUpdate() {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, whatIsGround);
    }


    public void UpdatePhysics(Vector2 move, float lookX, bool jump) {
        // Move
        Vector3 position = (transform.right * move.x) + (transform.forward * move.y);
        characterController.Move(position * moveSpeed * Time.fixedDeltaTime);

        // Gravity and Jump
        if (isGrounded) {
            if (jump) {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
            else if (velocity.y < 0) {
                velocity.y = -2f;
            }
        }

        velocity.y += gravity * Time.fixedDeltaTime;
        characterController.Move(velocity * Time.fixedDeltaTime);

        // Rotação do player
        transform.Rotate(Vector3.up * lookX);      

    }

    private void OnAnimatorMove() {
        //Update the position based on the next position;
        //characterController.Move(_movement.nextPosition * Time.deltaTime);
        //transform.position = _movement.nextPosition;
    }
}
