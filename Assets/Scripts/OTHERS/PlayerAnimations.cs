using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimations : MonoBehaviour {
    
    [Header("Player Animations")]
    public Vector2 velocity = Vector2.zero;
    public float magnitude = 0.25f;
    public float rotationLerp;

    [Header(" - Config")]
    [SerializeField] private Animator animator;

    private Vector2 smoothDeltaPosition = Vector2.zero;

    public bool shouldMove;

    public Transform look;

    public GameObject arrow;

    public Transform arrowBone;
    public Transform character;
    public GameObject arrowPrefab;

    Vector3 oldPosition;

    // Turn left/right, walk all directions, jump, gethit, death

    private void LateUpdate() {
        oldPosition = transform.position;

    }
    public void UpdateAnimation(Vector2 move, bool aim) {
        SetCharacterRotation(move, aim);
        Animation(aim);
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

    private void Animation(bool isAiming) {
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

        shouldMove = velocity.magnitude > magnitude;


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
        animator.SetFloat(Constants.Anim.VELOCITY_Y, Mathf.Abs(velocity.y));
    }

    public void Fire() {
        animator.SetTrigger(Constants.Anim.FIRE);
        arrow.SetActive(false);
        StartCoroutine(FireArrow());
    }

    private void OnAnimatorMove() {
        print("alo");
        //Update the position based on the next position;
        //characterController.Move(_movement.nextPosition * Time.deltaTime);
        //transform.position = _movement.nextPosition;
    }

    private void OnAnimatorIK(int layerIndex) {
        print("IK + " + layerIndex);

    }

    [SerializeField] private Transform firePoint;

    IEnumerator FireArrow()
    {
        GameObject projectile = Instantiate(arrowPrefab);
        projectile.transform.forward = look.forward;
        projectile.transform.position = firePoint.position + firePoint.forward;
        //Wait for the position to update
        yield return new WaitForSeconds(0.1f);

        projectile.GetComponent<ArrowProjectile>().Fire();
        
    }

    private void Die() {
        animator.SetTrigger("Die    ");
    }


}
