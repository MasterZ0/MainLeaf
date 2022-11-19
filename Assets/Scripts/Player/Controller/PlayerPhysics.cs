using System;
using AdventureGame.Data;
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
    public class PlayerPhysics : PlayerClass
    {
        [Title("Layers")]
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private LayerMask playerInvincible;
        [SerializeField] private LayerMask playerDefault;

        [Title("Components")]
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Transform groundCheck;

        private float gravityScale;
        private float moveSpeed;
        private float verticalVelocity;
        private float targetYRotation;
        private float rotationVelocity;

        #region Properties and Const
        public Vector3 Velocity => characterController.velocity;

        private Transform Transform => characterController.transform;
        private PlayerPhysicsSettings Settings => Controller.PlayerSettings.Physics;
        private float Weight => Settings.Mass * Physics.gravity.y;
        private float EulerYCamera => Controller.Camera.CameraTarget.eulerAngles.y;
        #endregion

        #region Methods
        public void PlayerDefaultLayer() => Controller.gameObject.layer = playerDefault.ToIntLayer();
        public void PlayerInvincibleLayer() => Controller.gameObject.layer = playerInvincible.ToIntLayer();
        public bool IsPlayerInvincible() => Controller.gameObject.layer == playerInvincible.ToIntLayer();

        public void SetGravityScale(float gravityScale) => this.gravityScale = gravityScale;

        public void Knockback(float maxDisplacement, Vector3 direction)
        {
            //playerCollider.Knockback(maxDisplacement, groundLayer, direction);
        }
        #endregion


        #region Public methods
        internal void FixedMove(float speed)
        {
            // Movement
            Vector2 direction = Controller.Inputs.Move;

            if (direction != Vector2.zero)
            {
                moveSpeed = speed;
                if (direction.magnitude > 1)
                {
                    direction = direction.normalized;
                }
                targetYRotation = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + EulerYCamera;
            }

            // Rotation
            if (!Controller.Camera.YLocked)
            {
                float rotation = Mathf.MoveTowardsAngle(Transform.eulerAngles.y, EulerYCamera, Settings.AimBodyCorrectionSpeed);
                Transform.rotation = Quaternion.Euler(0f, rotation, 0f);

                if (MathUtils.AngleDiference(EulerYCamera, Transform.eulerAngles.y) <= Settings.FullLockAngle)
                {
                    Controller.Camera.LockY(true);
                }
            }
            else
            {
                float yRotation = Controller.Inputs.Look.x * Controller.Sensitivity + Transform.eulerAngles.y;
                Transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
            }
        }

        internal void Move(float speed)
        {
            Vector2 direction = Controller.Inputs.Move;

            if (direction == Vector2.zero)
                return;

            moveSpeed = speed;
            if (direction.magnitude > 1)
            {
                direction = direction.normalized;
            }

            targetYRotation = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + EulerYCamera;

            // Rotation
            float rotation = Mathf.SmoothDampAngle(Transform.eulerAngles.y, targetYRotation, ref rotationVelocity, Settings.RotationSmoothTime);
            Transform.rotation = Quaternion.Euler(0f, rotation, 0f);
        }

        internal void Update()
        {
            UpdateVerticalVelocity();
            float speed = GetAccelerationSpeed(moveSpeed);

            // Movement X Z
            Vector3 targetDirection = Quaternion.Euler(0f, targetYRotation, 0f) * Vector3.forward;
            Vector3 velocity = targetDirection.normalized * speed;
            velocity.y = verticalVelocity;

            characterController.Move(velocity * Time.fixedDeltaTime);

            moveSpeed = 0f;
        }

        public void Jump(float jumpHeight) // TODO: What is -2?
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * Weight);
        }

        public bool CheckGround()
        {
            return Physics.CheckSphere(groundCheck.position, Settings.GroundCheckRadius, groundLayer);
        }
        #endregion

        #region Private Methods
        /// <summary> Gravity and Jump Velocity </summary>
        private void UpdateVerticalVelocity()
        {
            if (CheckGround() && verticalVelocity < 0f) // Slope force?
            {
                verticalVelocity = -2f;
            }

            float maxFallingVelocity = -Settings.MaxFallingVelocity;

            verticalVelocity += Weight * gravityScale * Time.fixedDeltaTime;
            if (verticalVelocity < maxFallingVelocity)
            {
                verticalVelocity = maxFallingVelocity;
            }
        }

        private float GetAccelerationSpeed(float targetSpeed) // TODO: acceleration = 6, deceleration = 3
        {
            // a reference to the players current horizontal velocity
            float currentHorizontalSpeed = new Vector3(Velocity.x, 0f, Velocity.z).magnitude;

            //float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;
            float inputMagnitude = 1f;
            float transition = targetSpeed > currentHorizontalSpeed ? Settings.Acceleration : Settings.Deceleration;

            return Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, transition * Time.fixedDeltaTime);
        }
        #endregion

        #region Gizmos
        public void DrawGizmos()
        {
            Gizmos.DrawWireSphere(groundCheck.position, Settings.GroundCheckRadius); 
        }
        #endregion

    }
}
