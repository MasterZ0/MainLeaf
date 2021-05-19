using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public abstract class PlayerInputs : GameObserver {
    [Header("Player Inputs")]
    [SerializeField] protected PlayerSettings playerSettings;

    protected Vector2 Look { get; private set; }
    protected Vector2 Move { get; private set; }

    private Controls controls;

    protected virtual void Start() {
        controls = new Controls();
        controls.Player.Jump.started += ctx => OnJump();
        controls.Player.Sprint.started += ctx => OnSprint(true);
        controls.Player.Sprint.canceled += ctx => OnSprint(false);
        controls.Player.Aim.started += ctx => OnAim(true);
        controls.Player.Aim.canceled += ctx => OnAim(false);
        controls.Player.Fire.started += ctx => OnFire(true);
        controls.Player.Move.performed += ctx => Move = ctx.ReadValue<Vector2>(); 
        controls.Player.Look.performed += ctx => Look = ctx.ReadValue<Vector2>();

        if (!playerSettings.showMouse) {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
        }

        if (playerSettings.ignoreTime)
            controls.Enable();
    }
    //public void SetControlsActive(bool active) {
    //    if (active) {
    //        controls.Enable();
    //    }
    //    else {
    //        controls.Disable();
    //    }
    //}
    protected abstract void OnSprint(bool sprit);
    protected abstract void OnFire(bool fire);
    protected abstract void OnJump();
    protected abstract void OnAim(bool active);

    protected override void OnPlaying() {
        controls.Enable();
    }
    protected override void OnGameOver() {
        controls.Disable();
    }

    protected override void OnDestroy() {
        base.OnDestroy();
        controls.Dispose();
    }
}
