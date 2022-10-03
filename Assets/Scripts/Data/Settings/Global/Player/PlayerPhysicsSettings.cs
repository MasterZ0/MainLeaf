using Sirenix.OdinInspector;
using UnityEngine;

namespace AdventureGame.Data
{
    [System.Serializable]
    public class PlayerPhysicsSettings
    {
        [FoldoutGroup(Movement)]
        [SerializeField, Range(0f, 50f)] private float groundMoveVelocity = 4f;
        [FoldoutGroup(Movement)]
        [SerializeField, Range(0f, 10f)] private float airMoveVelocity = 4f;
        [FoldoutGroup(Movement), Range(0f, 10f)]
        [SerializeField] private float ropeAirBoost = 3f;
        [FoldoutGroup(Movement), Range(0f, 10f)]
        [SerializeField] private float airBoostDamp = 3f;
        [FoldoutGroup(Movement)]
        [SerializeField, Range(0f, 10f)] private float blockingMoveVelocity = 2f;

        [FoldoutGroup(Gravity)]
        [SerializeField, Range(5f, 20f)] private float maxFallingVelocity = 11f;
        [FoldoutGroup(Gravity)]
        [SerializeField, Range(0f, 6f)] private float groundedGravity = 3f;
        [FoldoutGroup(Gravity)]
        [SerializeField, Range(0f, 6f)] private float airGravity = 2.5f;
        [FoldoutGroup(Gravity), Tooltip("First Jump, Second Jump, Dodge, Strong Shoot")]
        [SerializeField, Range(0f, 6f)] private float jumpGravity = 1f;
        [FoldoutGroup(Gravity)]
        [SerializeField, Range(0f, 6f)] private float hyperJumpGravity = 1f;

        [FoldoutGroup(Jump)]
        [SerializeField, Range(0f, 4f)] private float minimumJumpHeight = 0.3f;
        [FoldoutGroup(Jump)]
        [SerializeField, Range(0f, 20f)] private float firstJumpVelocity = 5.4f;
        [FoldoutGroup(Jump)]
        [SerializeField, Range(0f, 20f)] private float secondJumpVelocity = 5.4f;
        [FoldoutGroup(Jump)]
        [SerializeField, Range(0f, 20f)] private float hyperJumpVelocity = 11f;
        [FoldoutGroup(Jump)]
        [SerializeField, Range(0f, 20f)] private float hyperJumpControlVelocity = 7.5f;

        [FoldoutGroup(GroundRules), SuffixLabel("°")]
        [SerializeField, Range(0, 75f)] private int maxSlopeAngle = 50;
        [FoldoutGroup(GroundRules)]
        [SerializeField, Range(0f, 1f)] private float raycastGroundLength = 0.07f; // old 0.1, try 0.04 ~ 0.07
        [FoldoutGroup(GroundRules)]
        [SerializeField, Range(0f, 1f)] private float raycastAirGroundLength = 0.07f; // old 0.1, try 0.04 ~ 0.07
        [FoldoutGroup(GroundRules)]
        [SerializeField, Range(0f, 1f)] private float raycastSlopLength = 0.03f;

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
        [SerializeField, Range(0f, 10f)] private float dodgeXVelocity = 8f;
        [FoldoutGroup(Dodge)]
        [SerializeField, Range(0f, 10f)] private float minimumDodgeXVelocity = 3f;
        [FoldoutGroup(Dodge)]
        [SerializeField, Range(0f, 10f)] private float dodgeDamp = 9f;
        [FoldoutGroup(Dodge)]
        [SerializeField, Range(0f, 2f)] private float minDodgeDuration = 0.2f;

        [FoldoutGroup(Battle)]
        [VectorSlider(-0, 20f)]
        [SerializeField] private Vector2 strongShootJumpVelocity = new Vector2(6f, 2f);
        [FoldoutGroup(Battle)]
        [SerializeField] private float strongShootRotationDegrees = -25f;
        [FoldoutGroup(Battle)]
        [SerializeField, Range(0f, 10f)] private float strongShootJumpGravity = 0.5f;
        [FoldoutGroup(Battle)]
        [SerializeField, Range(0f, 10f)] private float strongShootFallingGravity = 3f;

        [FoldoutGroup(Rope)]
        [SerializeField] private float angularVelocity = 7f;
        [FoldoutGroup(Rope)]
        [SerializeField] private float ropeExponent = 0.3f;
        [FoldoutGroup(Rope)]
        [SerializeField] private float correctionSmoothness = 1f;

        [FoldoutGroup(Injury), Range(0f, 5f)]
        [SerializeField] private float invincibilityTimeAfterInjury = 1f;
        [FoldoutGroup(Injury), MinMaxSlider(0.01f, 5f, true)]
        [SerializeField] private Vector2 criticalInjuryStunTime = new Vector2(.2f, 2f);
        [FoldoutGroup(Injury), VectorSlider(0f, 10f)]
        [SerializeField] private Vector2 criticalInjuryVelocity = new Vector2(5f, 5f);
        [FoldoutGroup(Injury), Range(0.01f, 2f)]
        [SerializeField] private float slightInjuryTime = 0.2f;
        [FoldoutGroup(Injury), Range(0.01f, 1f)]
        [SerializeField] private float slightInjuryDisplacement = 0.15f;
        [FoldoutGroup(Injury), Range(0.01f, 1f)]
        [SerializeField] private float dyingMinTime = 0.2f;
        [FoldoutGroup(Injury), VectorSlider(0f, 10f)]
        [SerializeField] private Vector2 deathVelocity = new Vector2(6f, 6f);

        [FoldoutGroup(HitEffect), Range(0f, 10f)]
        [SerializeField] private float spiderStunMaxDuration = 5f;
        [FoldoutGroup(HitEffect), Range(0, 10)]
        [SerializeField] private int movesToEscapeSpiderStun = 5;


        public float InvincibilityTimeAfterInjury => invincibilityTimeAfterInjury;
        public Vector2 CriticalInjuryStunTime => criticalInjuryStunTime;
        public Vector2 CriticalInjuryVelocity => criticalInjuryVelocity;
        public float SlightInjuryTime => slightInjuryTime;
        public float SlightInjuryDisplacement => slightInjuryDisplacement;
        public float DyingMinTime => dyingMinTime;
        public Vector2 DeathVelocity => deathVelocity;
        public float AirBoostDamp => airBoostDamp;
        public float RopeAirBoost => ropeAirBoost;
        public float AngularVelocity => angularVelocity;
        public float RopeExponent => ropeExponent;
        public float CorrectionSmoothness => correctionSmoothness;
        public int MaxSlopeAngle => maxSlopeAngle;
        public float RaycastSlopLength => raycastSlopLength;
        public float RaycastGroundLength => raycastGroundLength;
        public float RaycastBoxLength => raycastBoxLength;
        public float RaycastAirGroundLength => raycastAirGroundLength;


        //Movement
        public float GroundMoveVelocity => groundMoveVelocity;
        public float AirMoveVelocity => airMoveVelocity;
        public float BlockingMoveVelocity => blockingMoveVelocity;

        //Jump
        public float MinimumJumpHeight => minimumJumpHeight;
        public float FirstJumpVelocity => firstJumpVelocity;
        public float SecondJumpVelocity => secondJumpVelocity;
        public float HyperJumpVelocity => hyperJumpVelocity;
        public float HyperJumpMinVelocity => hyperJumpControlVelocity; 
        public float MaxFallingVelocity => maxFallingVelocity;
        public float AirDashVelocity => airDashVelocity;
        public float AirDashDuration => airDashDuration;
        public float GroundDashVelocity => groundDashVelocity;
        public float GroundDashDuration => groundDashDuration;

        //Dodge
        public float DodgeYVelocity => dodgeYVelocity;
        public float DodgeXVelocity => dodgeXVelocity;
        public float DodgeDamp => dodgeDamp;
        public float MinDodgeDuration => minDodgeDuration;
        public float MinimumDodgeXVelocity => minimumDodgeXVelocity;

        //Battle
        public Vector2 StrongShootJumpVelocity => strongShootJumpVelocity;
        public float StrongShootRotationDegrees => strongShootRotationDegrees;
        public float StrongShootJumpGravity => strongShootJumpGravity;
        public float StrongShootFallingGravity => strongShootFallingGravity;

        //Rope
        public float GroundedGravity => groundedGravity;
        public float AirGravity => airGravity;
        public float JumpGravity => jumpGravity;
        public float HyperJumpGravity => hyperJumpGravity;

        //HitEffect
        public float SpiderStunMaxDuration => spiderStunMaxDuration;
        public int MovesToEscapeSpiderStun => movesToEscapeSpiderStun;

        private const string Battle = "Battle";
        private const string Movement = "Movement";
        private const string GroundRules = "Ground Rules";
        private const string Gravity = "Gravity";
        private const string Dash = "Dash";
        private const string Jump = "Jump";
        private const string Dodge = "Dodge";
        private const string Rope = "Rope";
        private const string Injury = "Injury";
        private const string HitEffect = "HitEffect";
    }
}
