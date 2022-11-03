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
    public class CameraController
    {
        [Title("Camera Controller")]
        [SerializeField] private LayerMask hittableLayer;
        [SerializeField] private Transform cameraTarget;
        [SerializeField] private Transform aimPoint;
        [SerializeField] private GameObject defaultCamera;

        private GameObject currentCamera;

        private PlayerController controller;

        private float sensitivity;
        private float xRotation;
        private float yRotation;

        public bool XLocked { get; private set; }
        public bool YLocked { get; private set; }

        private const float Threshold = 0.01f;

        public Transform CameraTarget => cameraTarget;
        private PlayerPhysicsSettings Settings => controller.PlayerSettings.Physics;

        #region Initialization
        public void Init(PlayerController playerController)
        {
            defaultCamera.transform.parent.SetParent(null);
            controller = playerController;

            currentCamera = defaultCamera;

            yRotation = cameraTarget.eulerAngles.y;
            SetSensitivity(Settings.DefaultSensibility);
            //Inputs.OnMoveCamera += UpdateCameraRotation;
        }

        public void Destroy()
        {
            //Object.Destroy(defaultCamera.transform.parent.gameObject);
        }
        #endregion

        public void SwitchCamera(GameObject newCamera)
        {
            currentCamera.SetActive(false);
            currentCamera = newCamera;
            currentCamera.SetActive(true);
        }

        public void LateUpdate()
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

        public void SetSensitivity(float newSensitivity) => sensitivity = newSensitivity;

        public void UpdateCameraRotation() // Maybe laye update
        {
            Vector2 look = controller.Inputs.Look;

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

        public void LockY(bool locked) => YLocked = locked;
        
    }
}