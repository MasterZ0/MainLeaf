using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectil : PooledObject {
    [Header("Arrow Projectile")]
    [SerializeField] private int damage;
    [SerializeField] private float force;

    [Header(" - Config")]
    [SerializeField] private Rigidbody rigidbod;
    [SerializeField] private PooledObject impactEffect;

    protected override void StartObject() {
        rigidbod.isKinematic = false;
        rigidbod.AddForce(transform.forward * (100 * Random.Range(1.3f, 1.7f)), ForceMode.Impulse);
    }

    public void OnCollisionEnter(Collision collision) {
        rigidbod.isKinematic = true;

        if (collision.gameObject.CompareTag(Constants.Tag.PLAYER)) {
            collision.gameObject.GetComponent<PlayerPhysics>().TakeDamage(damage);
        }

        impactEffect.SpawObject(transform.position, Quaternion.identity);
        ReturnToPool();
    }
}
