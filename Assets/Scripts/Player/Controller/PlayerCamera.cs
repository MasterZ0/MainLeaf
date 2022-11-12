using AdventureGame.Data;
using AdventureGame.Gameplay;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureGame.Player
{
    [Serializable]
    [FoldoutGroup("Camera Controller"), HideLabel, InlineProperty]
    public class PlayerCamera : PlayerClass
    {
        [Title("Camera")]
        [SerializeField] private LayerMask hittableLayer;
        [SerializeField] private Transform cameraTarget;
        [SerializeField] private Transform aimPoint;
        [SerializeField] private GameObject defaultCamera;

        public bool XLocked { get; private set; }
        public bool YLocked { get; private set; }

        private GameObject currentCamera;

        private float sensitivity;
        private float xRotation;
        private float yRotation;

        private const float Threshold = 0.01f;
        public Transform CameraTarget => cameraTarget;
        private PlayerPhysicsSettings Settings => Controller.PlayerSettings.Physics;

        #region Initialization
        public override void Init(PlayerController playerController)
        {
            base.Init(playerController);
            defaultCamera.transform.parent.SetParent(null);

            currentCamera = defaultCamera;

            yRotation = cameraTarget.eulerAngles.y;
            SetSensitivity(Settings.DefaultSensibility);
        }
        #endregion

        public void SwitchCamera(GameObject newCamera)
        {
            currentCamera.SetActive(false);
            currentCamera = newCamera;
            currentCamera.SetActive(true);
        }

        public void LockY(bool locked) => YLocked = locked;

        public void SetSensitivity(float newSensitivity) => sensitivity = newSensitivity;

        public void Update()
        {
            UpdateAimPoint();
            UpdateCameraRotation();
        }

        private void UpdateAimPoint()
        {
            Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Ray ray = MainCamera.Camera.ScreenPointToRay(screenCenterPoint);
            if (Physics.Raycast(ray, out RaycastHit hit, Settings.MaxAimDistance, hittableLayer))
            {
                aimPoint.position = hit.point;
            }
            else
            {
                aimPoint.position = MainCamera.Position + MainCamera.Instance.transform.forward * Settings.MaxAimDistance;
            }
        }

        private void UpdateCameraRotation() // Maybe laye update
        {
            Vector2 look = Controller.Inputs.Look;

            if (look.sqrMagnitude >= Threshold)
            {
                // TODO: Use delta time if is joystick
                //float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

                if (!XLocked)
                {
                    xRotation -= look.y * sensitivity;
                    xRotation = Mathf.Clamp(xRotation, Settings.CameraRangeRotation.x, Settings.CameraRangeRotation.y);
                }

                if (!YLocked)
                {
                    yRotation += look.x * sensitivity;
                    yRotation = ClampAngle(yRotation);
                }
                else
                {
                    yRotation = cameraTarget.eulerAngles.y;
                }
            }

            cameraTarget.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        }

        private static float ClampAngle(float angle)
        {
            if (angle < -360f)
                angle += 360f;

            if (angle > 360f)
                angle -= 360f;

            return angle;
        }
    }
}