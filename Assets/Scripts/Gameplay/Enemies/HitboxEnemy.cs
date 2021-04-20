using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxEnemy : MonoBehaviour {

    [Header("Hitbox Enemy")]
    [SerializeField] private int damage;

    //[Header(" - Config")]
    //[SerializeField] private PooledObject impactEffect;

    private void OnTriggerEnter(Collider col) {

        if (col.CompareTag(Constants.Tag.PLAYER)) {
            col.GetComponent<IDamageable>().TakeDamage(new Damage(damage));
        }
    }
    public void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag(Constants.Tag.PLAYER)) {
            collision.gameObject.GetComponent<IDamageable>().TakeDamage(new Damage(damage));
            //impactEffect.SpawnObject(transform.position, transform.rotation);
        }
    }

}
