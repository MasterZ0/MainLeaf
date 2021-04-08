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
    [SerializeField] private AimController aimController;
    [SerializeField] private PlayerPhysics playerPhysics;
    [SerializeField] private PlayerAnimations playerAnimations;


    private Vector2 look;
    private Vector2 move;
    private Controls controls;

    private bool jump;
    private bool aim;
    void Start() {
        controls = new Controls();
        controls.Enable();
        controls.Player.Jump.started += ctx => OnJump(true);
        controls.Player.Jump.canceled += ctx => OnJump(false);
        controls.Player.Aim.started += ctx => OnAim(true);
        controls.Player.Aim.canceled += ctx => OnAim(false);
        controls.Player.Fire.started += ctx => OnFire();

        if(hideMouse)
            Cursor.visible = false;

        SetControlsActive(true);
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
        if (aim)
            playerAnimations.Fire();
    }

    private void OnAim(bool active) {
        aim = active;
        aimController.OnAim(aim);
    }

    private void OnJump(bool active) {
        jump = active;
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
        playerAnimations.UpdateAnimation(move, aim);
        playerPhysics.UpdatePhysics(move, look.x, jump);
        aimController.UpdateCameraRotation(look.y); // Rotação da camera
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
