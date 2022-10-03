using AdventureGame.Shared.ExtensionMethods;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    [Header("Arrow Projectile")]
    [SerializeField] private int damage;
    [SerializeField] private float force;
    [SerializeField] private Vector2 forceRange = new Vector2(1.3f, 1.7f);

    [Header(" - Config")]
    [SerializeField] private Transform impactEffect;
    [SerializeField] private Rigidbody rigidbod;

    private Transform sender;
    private void Awake() {
        rigidbod.centerOfMass = transform.position;
    }

    private void OnEnable()
    {
        rigidbod.isKinematic = false;

        float finalForce = force + forceRange.RandomRange();

        rigidbod.AddForce(transform.forward * finalForce, ForceMode.Impulse);
    }

    public void Fire(Transform sender) {
        this.sender = sender;
    }
    //private void OnTriggerEnter(Collider col) {
    //    print(col.name);
    //    rigidbod.velocity = Vector3.zero;
    //    rigidbod.isKinematic = true;

    //    if (col.CompareTag(Constants.Tag.ENEMY)) {
    //        col.GetComponent<IDamageable>().TakeDamage(damage);
    //        StartCoroutine(Countdown(.5f));
    //    }
    //    else {
    //        StartCoroutine(Countdown(3));
    //    }
    //}
    public void OnCollisionEnter(Collision collision) {
        rigidbod.isKinematic = true;

        //if (collision.gameObject.CompareTag(Constants.Tag.ENEMY)) {
        //    collision.gameObject.GetComponent<IDamageable>().TakeDamage(new Damage(sender, damage, collision.transform.position));
        //    impactEffect.SpawnObject(transform.position, transform.rotation);
        //    ReturnToPool();
        //}
        //else {
        //    StartCoroutine(Countdown(3));
        //}
    }

    //IEnumerator Countdown(float countdown) {
    //    yield return new WaitForSeconds(countdown);
    //    ReturnToPool();
    //}

}
