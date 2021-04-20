using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour {
    [Header("PlayerInputs")]
    [SerializeField] private bool hideMouse;
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
        if(hideMouse)
            Cursor.visible = false;

        playerAnimations.Init();
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


    //private void OldCode() {
    //    #region Player Based Rotation

    //    //Move the player based on the X input on the controller
    //    //transform.rotation *= Quaternion.AngleAxis(_look.x * rotationPower, Vector3.up);
    //    //Rotate the Follow Target transform based on the input
    //    followTransform.transform.rotation *= Quaternion.AngleAxis(look.x * rotationPower, Vector3.up);
    //    #endregion

    //    #region Follow Transform Rotation



    //    #endregion

    //    #region Vertical Rotation
    //    followTransform.transform.rotation *= Quaternion.AngleAxis(-look.y * rotationPower, Vector3.right);

    //    var angles = followTransform.transform.localEulerAngles;
    //    angles.z = 0;

    //    var angle = followTransform.transform.localEulerAngles.x;

    //    //Clamp the Up/Down rotation
    //    if (angle > 180 && angle < 340) {
    //        angles.x = 340;
    //    }
    //    else if (angle < 180 && angle > 40) {
    //        angles.x = 40;
    //    }


    //    followTransform.transform.localEulerAngles = angles;
    //    #endregion


    //    nextRotation = Quaternion.Lerp(followTransform.transform.rotation, nextRotation, Time.deltaTime * rotationLerp);

    //    if (move.x == 0 && move.y == 0) {
    //        nextPosition = transform.position;

    //        if (aimValue) {
    //            //Set the player rotation based on the look transform
    //            transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);
    //            //reset the y rotation of the look transform
    //            followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
    //        }

    //        return;
    //    }
    //    float moveSpeed = walkSpeed * Time.deltaTime;
    //    Vector3 position =  (transform.right * move.x * moveSpeed) + (transform.forward * move.y * moveSpeed);
    //    nextPosition = transform.position + position;


    //    //Set the player rotation based on the look transform
    //    transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);
    //    //reset the y rotation of the look transform
    //    followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
    //}
}
