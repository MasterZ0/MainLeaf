using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorController : PlayerInputs {

    [Header("Archer Controller")]
    [SerializeField] private CameraController cameraController;
    [SerializeField] private PlayerPhysics playerPhysics;
    [SerializeField] private PlayerAnimations playerAnimations;

    private bool aim;
    private bool sprint;
    private bool isDead;

    protected override void OnGameOver() {
        base.OnGameOver();
        cameraController.PlayerDeath();
        playerPhysics.PlayerDeath();
        playerAnimations.PlayerDeath();
        isDead = true;
    }
    protected override void OnSprint(bool sprint) {
        if (aim)
            return;

        cameraController.SetSprint(sprint);
    }

    protected override void OnFire(bool fire) {
        if (aim && playerPhysics.Fire()) {
            playerAnimations.Fire();
        }
    }

    protected override void OnJump() {
        if (playerPhysics.CanJump()) {
            playerAnimations.Jump();
        }
    }

    protected override void OnAim(bool active) {
        if (active && (playerPhysics.isGrounded || playerPhysics.isJumping)) {
            aim = true;
        }
        else {
            aim = false;
        }
        cameraController.SetAim(aim);
    }

    private void FixedUpdate() {
        if (isDead)
            return;

        Vector2 look = Look;
        if (aim) {
            look *= playerSettings.aimMouseSensitivity * Time.fixedDeltaTime;
        }
        else {
            look *= playerSettings.walkMouseSensitivity * Time.fixedDeltaTime;
        }

        // Conditions
        bool isGrounded = playerPhysics.isGrounded;
        if (aim && (!isGrounded || playerPhysics.isJumping)) {
            OnAim(false);
        }

        // Physics
        Vector3 stepDistance = transform.position;
        playerPhysics.UpdatePhysics(Move, look.x, sprint, aim);
        stepDistance = transform.position - stepDistance;

        // Anim
        playerAnimations.UpdateAnimation(Move, stepDistance, aim, isGrounded);

        // Cam
        cameraController.UpdateCameraRotation(look.y);
    }
}
