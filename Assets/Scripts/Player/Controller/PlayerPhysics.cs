using System;
using System.Collections.Generic;
using AdventureGame.Data;
using AdventureGame.Inputs;
using AdventureGame.Shared.ExtensionMethods;
using AdventureGame.Shared.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AdventureGame.Player
{
    /// <summary>
    /// Handles player physics
    /// </summary>
    [Serializable]
    [FoldoutGroup("Physics"), HideLabel, InlineProperty]
    public class PlayerPhysics
    {
        [Title("Layers")]
        [SerializeField] private CharacterController characterController;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private LayerMask playerInvincible;
        [SerializeField] private LayerMask playerDefault;

        [Title("Physic Materials")]
        [SerializeField] private PhysicMaterial fullFrictionMaterial;
        [SerializeField] private PhysicMaterial zeroFrictionMaterial;

        [Title("Test")]
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundRadius = .02f;
        #region Properties
        private PlayerPhysicsSettings Settings => controller.PlayerSettings.Physics;
        public Transform Transform => characterController.transform;
        public Vector3 Velocity => characterController.velocity;

        /// <summary> Map to local space </summary>
       
        #endregion

        private float Weight => Settings.Mass * Physics.gravity.y;

        private PlayerController controller;
        private PlayerInputs Inputs => controller.Inputs;
        private float gravityScale;
        private Vector3 velocity;

        #region Methods
        public void Init(PlayerController playerController)
        {
            controller = playerController;
        }


        public void PlayerDefaultLayer() => controller.gameObject.layer = (int)Mathf.Log(playerDefault.value, 2);
        public void PlayerInvincibleLayer() => controller.gameObject.layer = (int)Mathf.Log(playerInvincible.value, 2);
        public bool IsPlayerInvincible() => controller.gameObject.layer == (int)Mathf.Log(playerInvincible.value, 2);
        public void FullFriction() => characterController.sharedMaterial = fullFrictionMaterial;
        public void NoFriction() => characterController.sharedMaterial = zeroFrictionMaterial;

        public void SetGravityScale(float gravityScale) => this.gravityScale = gravityScale;

        public void Knockback(float maxDisplacement, float direction)
        {
            //playerCollider.Knockback(maxDisplacement, groundLayer, direction);
        }
        #endregion



        #region Public methods
        internal void Move(Vector2 direction, float speed)
        {   
            direction = MathUtils.NormalizeCircle(direction); // D-pad or keyboard

            Vector3 motion = (Transform.right * direction.x) + (Transform.forward * direction.y);
            motion *= speed;
            velocity.x = motion.x;
            velocity.z = motion.z;
        }

        internal void Update()
        {
            // Camera / Character
            characterController.transform.Rotate(Vector3.up * Inputs.Look.x * Settings.RotationSpeed);

            // Gravity and Jump Velocity
            if (CheckGround() && velocity.y < 0) // Slope force?
            {
                velocity.y = -2f;
            }

            float maxFallingVelocity = -Settings.MaxFallingVelocity;
            if (velocity.y < maxFallingVelocity)
            {
                velocity.y = maxFallingVelocity;
            }


            velocity.y += Weight * gravityScale * Time.fixedDeltaTime;
            characterController.Move(velocity * Time.fixedDeltaTime);

            // TODO: acceleration = 6, deceleration = 3
            velocity.x = 0;
            velocity.z = 0;
        }

        public void Jump(float jumpHeight)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * Weight);
        }

        public bool CheckGround()
        {
            return Physics.CheckSphere(groundCheck.position, groundRadius, groundLayer);
        }
        #endregion

        #region Private Methods
        
        #endregion

        #region Gizmos
        public void DrawGizmos()
        {
            Gizmos.DrawWireSphere(groundCheck.position, groundRadius); 
        }
        #endregion

    }
}
