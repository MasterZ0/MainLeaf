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

        [Header("Parameters")]
        [SerializeField] private string moveSpeed = "MoveSpeed";
        [SerializeField] private string velocityX = "VelocityX";
        [SerializeField] private string velocityZ = "VelocityZ";

        [Header("States")]
        [SerializeField] private string jumpMoving = "Jump";
        [SerializeField] private string falling = "Falling";

        [SerializeField] private float animationBlendDamp = 0.3f;
        [SerializeField] private float smoothInputSpeed = 0.2f;
        [SerializeField] private float rotationLerp = 1;
        private PlayerController Controller { get; set; }
        private Transform AnimTransform => animator.transform;
        private PlayerInputs Inputs => Controller.Inputs;

        private float maxVelocityScale;




        public void Init(PlayerController controller  /*PlayerCharacteristicData visualData*/)
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
                Quaternion playerRotation = Quaternion.Lerp(AnimTransform.localRotation, Quaternion.identity, rotationLerp * Time.fixedDeltaTime);
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

            playerRotation = Quaternion.Lerp(AnimTransform.localRotation, playerRotation, rotationLerp * Time.fixedDeltaTime);
            AnimTransform.localRotation = playerRotation;
        }

        public void SetMaxVelocityScale(float newScale) => maxVelocityScale = newScale;

        public void Update()
        {
            //currentInputVector = Vector2.SmoothDamp(currentInputVector, input, ref smoothInputVelocity, smoothInputSpeed);
    
            // float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
            // velocity = Vector3.Lerp(smoothDeltaPosition, deltaPosition, smooth) / Time.deltaTime;
            Vector3 velocity = Controller.Physics.DeltaMove / maxVelocityScale;
            

            //smoothDeltaPosition = Vector2.Lerp(smoothDeltaPosition, deltaPosition, smooth);

            animator.SetFloat(moveSpeed, velocity.magnitude, animationBlendDamp , Time.fixedDeltaTime);
            animator.SetFloat(velocityX, velocity.x, animationBlendDamp, Time.fixedDeltaTime);
            animator.SetFloat(velocityZ, velocity.z, animationBlendDamp, Time.fixedDeltaTime);
        }
    }
}