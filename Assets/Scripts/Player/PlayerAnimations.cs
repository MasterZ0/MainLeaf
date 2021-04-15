using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(IDamageable))]
public class PlayerAnimations : MonoBehaviour {
    [Header("Player Animations")]
    public float magnitude = 0.25f;
    public float rotationLerp;

    [Header(" - Config")]
    [SerializeField] private Animator animator;
    [SerializeField] private Transform character;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform aimCamera;

    [Header(" - Prefab")]
    public float arrowMaxDistance = 40f;
    public LayerMask whatIsHittable;
    public PooledObject arrowPrefab;

    private Vector2 smoothDeltaPosition = Vector2.zero;
    private Vector2 velocity = Vector2.zero;

    private Quaternion defaultRotation;
    private bool ready;

    // Turn left/right, walk all directions, jump, gethit, death
    private IDamageable damageable;
    public void Init() {
        //int fire = Animator.StringToHash(Constants.Anim.FIRE);

        damageable = GetComponent<IDamageable>();
        damageable.DeathEvent += OnDamage;
    }

    void OnDamage() {

    }

    public void UpdateAnimation(Vector2 move, Vector3 oldPosition, bool aim, bool isGrounded) {
        if (!aim)
            ready = false;

        SetCharacterRotation(move, aim);
        Animation(oldPosition, aim);
        defaultRotation = firePoint.rotation;

        animator.SetBool(Constants.Anim.IS_GROUNDED, isGrounded);
    }

    private void SetCharacterRotation(Vector2 move, bool aim) {
        if (aim) { // Se estiver em Aim, o player deve sempre olhar pra frente

            if (character.localEulerAngles.y != 0) {
                Quaternion playerRotation = Quaternion.Lerp(character.localRotation, Quaternion.identity, rotationLerp * Time.fixedDeltaTime);
                character.localRotation = playerRotation;
            }
        }
        else if (move != Vector2.zero) { // Caso o contrario, o player deve deve olhar pra ultima direção que ele caminhou

            Vector3 direction = new Vector3(move.x, 0, move.y) - transform.eulerAngles;
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            Quaternion playerRotation = Quaternion.Euler(0, angle, 0);

            playerRotation = Quaternion.Lerp(character.localRotation, playerRotation, rotationLerp * Time.fixedDeltaTime);
            character.localRotation = playerRotation;
        }
    }
    public void Jump() {
        animator.SetTrigger(Constants.Anim.JUMP);
    }

    private void Animation(Vector3 worldDeltaPosition, bool isAiming) {
        //Vector3 worldDeltaPosition = transform.position - oldPosition; // nextposiotion?

        //Map to local space
        float dX = Vector3.Dot(transform.right, worldDeltaPosition);
        float dY = Vector3.Dot(transform.forward, worldDeltaPosition);
        Vector2 deltaPosition = new Vector2(dX, dY);

        float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
        smoothDeltaPosition = Vector2.Lerp(smoothDeltaPosition, deltaPosition, smooth);

        if (Time.deltaTime > 1e-5f) {
            velocity = smoothDeltaPosition / Time.deltaTime;
        }

        bool shouldMove = velocity.magnitude > magnitude;


        if (isAiming) {


            //if (animator.GetCurrentAnimatorStateInfo(2).IsName(Constants.Anim.FIRE)) {
            //    arrow.SetActive(false);
            //}
            //else {
            //    arrow.SetActive(true);
            //}
        }
        animator.SetBool(Constants.Anim.IS_AIMING, isAiming);

        animator.SetBool(Constants.Anim.IS_MOVING, shouldMove);
        animator.SetFloat(Constants.Anim.VELOCITY_X, velocity.x);
        animator.SetFloat(Constants.Anim.VELOCITY_Z, velocity.y);
        animator.SetFloat(Constants.Anim.MOVE_SPEED, velocity.magnitude / 7);
        //animator.SetBool(Constants.Anim.IS_MOVING, move != Vector2.zero);
        //animator.SetFloat(Constants.Anim.VELOCITY_X, move.x);
        //animator.SetFloat(Constants.Anim.VELOCITY_Y, move.y);
    }
    public void OnFire() {
        // Ammon --
        bool successful = Physics.Raycast(aimCamera.position, aimCamera.forward, out RaycastHit hit, arrowMaxDistance, whatIsHittable);
        if (successful) {
            firePoint.LookAt(hit.point);
            arrowPrefab.SpawObject(firePoint.position, firePoint.rotation);
        }
        else {
            arrowPrefab.SpawObject(firePoint.position, defaultRotation);
        }
    }

    public void OnReadyToShoot() {
        ready = true;
    }

    public void Fire() {
        if (ready) {
            ready = false;
            animator.SetTrigger(Constants.Anim.FIRE);
        }
    }
    private void Die() {
        animator.SetTrigger("Die");
    }

    public void OnFootR() {
    }
    public void OnFootL() {
    }

    private void OnDestroy() {
        if(damageable != null) {
            damageable.DeathEvent -= OnDamage;
        }
    }

    //Animator animator;
    //Transform rightHandObj;
    //Transform lookbObj;
    //private void OnAnimatorIK(int layerIndex) {
    //    bool Ik = true;
    //    if (Ik) {
    //        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
    //        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
    //        animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
    //        animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);

    //        animator.SetLookAtWeight(1);
    //        animator.SetLookAtPosition(lookbObj.position);

    //    }
    //    else {
    //        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
    //        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
    //        animator.SetLookAtWeight(0);

    //    }

    //}

    //public void OnAnimatorMove() {
    //    print("anim move");
    //}
}
