using AdventureGame.Data;
using AdventureGame.Inputs;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AdventureGame.Player
{
    public enum CameraType
    {
        Default,
        Sprint,
        Aim,
        Death
    }

    [Serializable]
    [FoldoutGroup("Camera Controller"), HideLabel, InlineProperty]
    public class CameraController
    {
        [Title("Camera Controller")]
        [SerializeField] private Transform cameraTarget;

        [Header(" - Cameras")]
        [SerializeField] private GameObject defaultCamera;
        [SerializeField] private GameObject sprintCamera;
        [SerializeField] private GameObject aimCamera;
        [SerializeField] private GameObject deathCamera;

        private Dictionary<CameraType, GameObject> cameras;
        private GameObject currentCamera;

        private PlayerInputs Inputs => controller.Inputs;
        private PlayerController controller;
        private float xRotation;

        private PlayerPhysicsSettings Settings => controller.PlayerSettings.Physics;

        public void Init(PlayerController playerController)
        {
            defaultCamera.transform.parent.SetParent(null);
            controller = playerController;

            currentCamera = defaultCamera;
            cameras = new Dictionary<CameraType, GameObject>()
            {
                [CameraType.Default] = defaultCamera,
                [CameraType.Sprint] = sprintCamera,
                [CameraType.Aim] = aimCamera,
                [CameraType.Death] = deathCamera,
            };

            //Inputs.OnMoveCamera += UpdateCameraRotation;
        }

        public void Destroy()
        {
            Object.Destroy(defaultCamera.transform.parent.gameObject);
        }

        public void Update()
        {
            UpdateCameraRotation(Inputs.Look.y);
        }

        private void UpdateCameraRotation(float lookY)
        {
            xRotation -= lookY * Settings.RotationSpeed;
            xRotation = Mathf.Clamp(xRotation, Settings.CameraRangeRotation.x, Settings.CameraRangeRotation.y);
            cameraTarget.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }

        public void SwitchCamera(CameraType cameraType)
        {
            currentCamera.SetActive(false);
            currentCamera = cameras[cameraType];
            currentCamera.SetActive(true);
        }
    }
}