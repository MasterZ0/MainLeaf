using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimations : MonoBehaviour {
    [Header("Player Animations")]
    public float magnitude = 0.25f;
    public float rotationLerp;

    [Header(" - Config")]
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject arrow;
    [SerializeField] private Transform character;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform look;

    [Header(" - Prefab")]
    public PooledObject arrowPrefab;

    private Vector2 smoothDeltaPosition = Vector2.zero;
    private Vector2 velocity = Vector2.zero;
    private Vector3 oldPosition;

    // Turn left/right, walk all directions, jump, gethit, death

    void Init() {
        int fire = Animator.StringToHash(Constants.Anim.FIRE);
        animator.GetBool(fire);
    }

    private void LateUpdate() {
        oldPosition = transform.position;

    }
    public void UpdateAnimation(Vector2 move, bool aim, Vector3 oldPosition) {
        SetCharacterRotation(move, aim);
        Animation(move, aim);
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

    private void Animation(Vector2 move, bool isAiming) {
        Vector3 worldDeltaPosition = oldPosition - transform.position; // nextposiotion?

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


            if (animator.GetCurrentAnimatorStateInfo(2).IsName(Constants.Anim.FIRE)) {
                arrow.SetActive(false);
            }
            else {
                arrow.SetActive(true);
            }
        }
        animator.SetBool(Constants.Anim.IS_AIMING, isAiming);

        animator.SetBool(Constants.Anim.IS_MOVING, shouldMove);
        animator.SetFloat(Constants.Anim.VELOCITY_X, velocity.x);
        animator.SetFloat(Constants.Anim.VELOCITY_Y, velocity.y);
        //animator.SetBool(Constants.Anim.IS_MOVING, move != Vector2.zero);
        //animator.SetFloat(Constants.Anim.VELOCITY_X, move.x);
        //animator.SetFloat(Constants.Anim.VELOCITY_Y, move.y);
    }

    public void Fire() {
        animator.SetTrigger(Constants.Anim.FIRE);
        arrow.SetActive(false);
        StartCoroutine(FireArrow());
    }

    
    IEnumerator FireArrow()
    {
        // spaw > rotation > position
        Vector3 position = firePoint.position + firePoint.forward;
        PooledObject projectile = arrowPrefab.SpawObject(position, Quaternion.identity);
        projectile.transform.forward = look.forward;
        //Wait for the position to update
        yield return new WaitForSeconds(0.1f);

        projectile.GetComponent<ArrowProjectile>().Fire();
        
    }

    private void Die() {
        animator.SetTrigger("Die");
    }

    public void OnFootR() {
    }
    public void OnFootL() {
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
