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

    protected override void OnEnablePooledObject() {
        rigidbod.isKinematic = false;
        rigidbod.AddForce(transform.forward * (force * Random.Range(1.3f, 1.7f)), ForceMode.Impulse);
    }


    public void OnCollisionEnter(Collision collision) {
        rigidbod.isKinematic = true;
        if (collision.gameObject.CompareTag(Constants.Tag.PLAYER)) {
            collision.gameObject.GetComponent<IDamageable>().TakeDamage(new Damage(gameObject, damage, collision.transform.position));
        }

        impactEffect.SpawnObject(transform.position, Quaternion.identity);
        ReturnToPool();
    }

    //private void OnTriggerEnter(Collider other) {
    //    rigidbod.isKinematic = true;

    //    if (other.CompareTag(Constants.Tag.PLAYER)) {
    //        other.GetComponent<IDamageable>().TakeDamage(new Damage());
    //    }

    //    impactEffect.SpawnObject(transform.position, Quaternion.identity);
    //    DesactivePooledObject();
    //}
}
