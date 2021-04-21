using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour {
    [Header("PlayerInputs")]
    [SerializeField] private bool ignoreTime;
    [SerializeField] private bool showMouse;
    [SerializeField] private float walkMouseSensitivity = 50;
    [SerializeField] private float aimMouseSensitivity = 2;

    [Header(" - Config")]
    [SerializeField] private CameraController cameraController;
    [SerializeField] private PlayerPhysics playerPhysics;
    [SerializeField] private PlayerAnimations playerAnimations;


    private Vector2 look;
    private Vector2 move;
    private Controls controls;

    private bool aim;
    private bool sprint;
    private bool isDead;
    void Start() {
        controls = new Controls();
        controls.Player.Sprint.performed += OnSprint;
        controls.Player.Jump.started += ctx => OnJump();
        controls.Player.Aim.started += ctx => OnAim(true);
        controls.Player.Aim.canceled += ctx => OnAim(false);
        controls.Player.Fire.started += ctx => OnFire();
        GameController.OnChangeState += OnChangeGameState; 
        // TODO: Transformar habilidades em Factory

        playerAnimations.Init();

        if (!showMouse) {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
        }

        if (ignoreTime)
            SetControlsActive(true);
    }
    private void OnChangeGameState(GameState gameState) {
        if (gameState == GameState.Playing) {
            controls.Enable();
            return;
        }

        if (gameState == GameState.PlayerDied) {
            cameraController.PlayerDeath();
            playerPhysics.PlayerDeath();
            playerAnimations.PlayerDeath();
            isDead = true;
        }
        controls.Disable();
    }

    public void OnSprint(InputAction.CallbackContext ctx) {
        if (aim)
            return;

        sprint = ctx.ReadValueAsButton();
        cameraController.SetSprint(sprint);
    }

    public void SetControlsActive(bool active) {
        if (active) {
            controls.Enable();
        }
        else {
            controls.Disable();
        }
    }
    private void OnFire() {
        if (aim && playerPhysics.Fire()) {
            playerAnimations.Fire();
        }
    }
    private void OnJump() {
        if (playerPhysics.CanJump()) {
            playerAnimations.Jump();
        }
    }
    private void OnAim(bool active) {
        if (active && (playerPhysics.isGrounded || playerPhysics.isJumping)) {
            aim = true;
        }
        else {
            aim = false;
        }
        cameraController.SetAim(aim);
    }


    void Update() {
        move = controls.Player.Move.ReadValue<Vector2>();
        look = controls.Player.Look.ReadValue<Vector2>();

        if (aim) {
            look *= aimMouseSensitivity * Time.deltaTime;
        }
        else {
            look *= walkMouseSensitivity * Time.deltaTime;
        }
    }

    private void FixedUpdate() {
        if (isDead)
            return;

        // Conditions
        bool isGrounded = playerPhysics.isGrounded;
        if(aim && (!isGrounded || playerPhysics.isJumping)) {
            OnAim(false);
        }

        // Physics
        Vector3 stepDistance = transform.position;
        playerPhysics.UpdatePhysics(move, look.x, sprint, aim);
        stepDistance = transform.position - stepDistance;

        // Anim
        playerAnimations.UpdateAnimation(move, stepDistance, aim, isGrounded);

        // Cam
        cameraController.UpdateCameraRotation(look.y); 
    }

    private void OnDestroy() {
        controls.Disable();
        //GameController.OnChangeState -= OnChangeGameState;

    }


}
