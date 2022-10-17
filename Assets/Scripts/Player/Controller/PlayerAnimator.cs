using AdventureGame.Data;
using AdventureGame.Persistence;
using AdventureGame.Shared.ExtensionMethods;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using AdventureGame.Inputs;

namespace AdventureGame.Player
{
    public enum AnimatorState // Changed animations : Jump stopped, Run/Walk forward, landing stopped
    {
        Standard,
        LoadingRequest,
        PlayingRequest
    }

    [Serializable]
    [FoldoutGroup("Animator"), HideLabel, InlineProperty]
    public class PlayerAnimator
    {
        [Header("Components")]
        [SerializeField] private Animator animator;

        [Header("States")]
        [SerializeField] private string jumpMoving = "Jump";
        [SerializeField] private string falling = "Falling";

        private PlayerController Controller { get; set; }
        private Transform AnimTransform => animator.transform;
        private PlayerInputs Inputs => Controller.Inputs;
        private float RotationBodyLerp => Controller.PlayerSettings.Physics.rotationBodyLerp;

        public void Init(PlayerController controller)
        {
            Controller = controller;
        }

        internal void Falling() => PlayAllLayers(falling);
        internal void Jump() => PlayAllLayers(jumpMoving);
        
        /// <param name="transitTime">0 - 1</param>
        public void PlayAllLayers(string stateName, float transitTime = 0.25f, int layerCount = 2)
        {
            for (int layerIndex = 0; layerIndex <= layerCount; layerIndex++)
            {
                AnimatorStateInfo current = animator.GetCurrentAnimatorStateInfo(layerIndex);
                animator.CrossFade(stateName, transitTime / current.length, layerIndex);
            }
        }

        internal void UpdateWalk()
        {
            if (AnimTransform.localEulerAngles.y != 0)
            {
                Quaternion playerRotation = Quaternion.Lerp(AnimTransform.localRotation, Quaternion.identity, RotationBodyLerp * Time.fixedDeltaTime);
                AnimTransform.localRotation = playerRotation;
            }
        }

        internal void UpdateRunBody()
        {
            if (!Inputs.MovePressed)
                return;

            Vector3 direction = new Vector3(Inputs.Move.x, 0, Inputs.Move.y) - AnimTransform.eulerAngles;
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            Quaternion playerRotation = Quaternion.Euler(0, angle, 0);

            playerRotation = Quaternion.Lerp(AnimTransform.localRotation, playerRotation, RotationBodyLerp * Time.fixedDeltaTime);
            AnimTransform.localRotation = playerRotation;
        }

    }
}