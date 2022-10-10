using Sirenix.OdinInspector;
using UnityEngine;

namespace AdventureGame.Data
{
    [System.Serializable]
    public class PlayerPhysicsSettings
    {
        [FoldoutGroup(Movement)]
        [SerializeField, Range(0f, 10f)] private float aimMoveSpeed = 2f;
        [FoldoutGroup(Movement)]
        [SerializeField, Range(0f, 20f)] private float walkSpeed = 7f;
        [FoldoutGroup(Movement)]
        [SerializeField, Range(0f, 20f)] private float sprintSpeed = 12f;

        [FoldoutGroup(Rotation)]
        [SerializeField] public float rotationSpeed = 1f;
        [FoldoutGroup(Rotation)]
        [MinMaxSlider(-90, 90, true)]
        [SerializeField] private Vector2 cameraRangeRotation = new Vector2(-30f, 30f);

        [FoldoutGroup(Gravity)]
        [SerializeField, Range(0f, 20f)] private float mass = 1f;
        [FoldoutGroup(Gravity)]
        [SerializeField, Range(5f, 20f)] private float maxFallingVelocity = 11f;
        [FoldoutGroup(Gravity)]
        [SerializeField, Range(0f, 6f)] private float groundedGravity = 3f;
        [FoldoutGroup(Gravity)]
        [SerializeField, Range(0f, 6f)] private float fallingGravity = 2.5f;
        [FoldoutGroup(Gravity)]
        [SerializeField, Range(0f, 6f)] private float jumpGravity = 1f;

        [FoldoutGroup(Jump)]
        [SerializeField, MinMaxSlider(0f, 4f, true)] private Vector2 jumpRangeDuration = new Vector2(.8f, 2f);
        [FoldoutGroup(Jump)]
        [SerializeField, Range(0f, 20f)] private float jumpVelocity = 5.4f;

        [FoldoutGroup(GroundRules)]
        [SerializeField, Range(0f, 1f)] private float groundCheckRadius = 0.02f;

        [FoldoutGroup(Dash)]
        [SerializeField, Range(0f, 20f)] private float airDashVelocity = 8f;
        [FoldoutGroup(Dash), SuffixLabel("s")]
        [SerializeField, Range(0f, 2f)] private float airDashDuration = 0.3f;
        [FoldoutGroup(Dash)]
        [SerializeField, Range(0f, 20f)] private float groundDashVelocity = 8f;
        [FoldoutGroup(Dash), SuffixLabel("s")]
        [SerializeField, Range(0f, 2f)] private float groundDashDuration = 0.4f;
        [FoldoutGroup(Dash)]
        [SerializeField, Range(0f, 2f)] private float raycastBoxLength = 1f;

        [FoldoutGroup(Dodge)]
        [SerializeField, Range(0f, 10f)] private float dodgeYVelocity = 4f;
        [FoldoutGroup(Dodge)]
        [SerializeField, Range(0f, 10f)] private float dodgeZVelocity = 8f;
        [FoldoutGroup(Dodge)]
        [SerializeField, Range(0f, 10f)] private float minimumDodgeXVelocity = 3f;
        [FoldoutGroup(Dodge)]
        [SerializeField, Range(0f, 10f)] private float dodgeDamp = 9f;
        [FoldoutGroup(Dodge)]
        [SerializeField, Range(0f, 2f)] private float minDodgeDuration = 0.2f;

        [FoldoutGroup(Injury), Range(0f, 5f)]
        [SerializeField] private float invincibilityTimeAfterInjury = 1f;
        [FoldoutGroup(Injury), VectorSlider(0f, 10f)]
        [SerializeField] private Vector2 injuryVelocity = new Vector2(5f, 5f);
        [FoldoutGroup(Injury), Range(0.01f, 2f)]
        [SerializeField] private float injuryTime = 0.2f;
        [FoldoutGroup(Injury), Range(0.01f, 1f)]
        [SerializeField] private float dyingMinTime = 0.2f;
        [FoldoutGroup(Injury), VectorSlider(0f, 10f)]
        [SerializeField] private Vector2 deathVelocity = new Vector2(6f, 6f);


        public float InvincibilityTimeAfterInjury => invincibilityTimeAfterInjury;
        public float DyingMinTime => dyingMinTime;
        public Vector2 DeathVelocity => deathVelocity;
        public float RaycastBoxLength => raycastBoxLength;


        //Movement
        public float AimMoveSpeed => aimMoveSpeed;
        public float WalkSpeed => walkSpeed;
        public float SprintSpeed => sprintSpeed;
        public float RotationSpeed => rotationSpeed;
        public Vector2 CameraRangeRotation => cameraRangeRotation;

        //Jump
        public float JumpVelocity => jumpVelocity;
        public Vector2 JumpRangeDuration => jumpRangeDuration;
        public float Mass => mass;
        public float MaxFallingVelocity => maxFallingVelocity;
        public float AirDashVelocity => airDashVelocity;
        public float AirDashDuration => airDashDuration;
        public float GroundDashVelocity => groundDashVelocity;
        public float GroundDashDuration => groundDashDuration;

        //Dodge
        public float DodgeYVelocity => dodgeYVelocity;
        public float DodgeZVelocity => dodgeZVelocity;
        public float DodgeDamp => dodgeDamp;
        public float MinDodgeDuration => minDodgeDuration;
        public float MinimumDodgeXVelocity => minimumDodgeXVelocity;

        public float GroundedGravity => groundedGravity;
        public float FallingGravity => fallingGravity;
        public float JumpGravity => jumpGravity;

        private const string Rotation = "Rotation";
        private const string Movement = "Movement";
        private const string GroundRules = "Ground Rules";
        private const string Gravity = "Gravity";
        private const string Dash = "Dash";
        private const string Jump = "Jump";
        private const string Dodge = "Dodge";
        private const string Injury = "Injury";
    }
}
