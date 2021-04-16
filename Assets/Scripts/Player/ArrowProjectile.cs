using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ArrowProjectile : PooledObject {
    [Header("Arrow Projectile")]
    [SerializeField] private int damage;
    [SerializeField] private float force;

    [Header(" - Config")]
    [SerializeField] private PooledObject impactEffect;
    [SerializeField] private Rigidbody rigidbod;
    [SerializeField] private CinemachineImpulseSource source;
    private void Awake() {
        rigidbod.centerOfMass = transform.position;
    }

    protected override void OnEnablePooledObject() {
        rigidbod.isKinematic = false;

        rigidbod.AddForce(transform.forward * (force * Random.Range(1.3f, 1.7f)), ForceMode.Impulse);
        source.GenerateImpulse(Camera.main.transform.forward);
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

        if (collision.gameObject.CompareTag(Constants.Tag.ENEMY)) {
            collision.gameObject.GetComponent<IDamageable>().TakeDamage(damage);
            impactEffect.SpawnObject(transform.position, transform.rotation);
            DesactivePooledObject();
        }
        else {
            StartCoroutine(Countdown(3));
        }
    }

    IEnumerator Countdown(float countdown) {
        yield return new WaitForSeconds(countdown);
        DesactivePooledObject();
    }

}
