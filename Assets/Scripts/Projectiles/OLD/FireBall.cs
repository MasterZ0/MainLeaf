using AdventureGame.ObjectPooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {
    [Header("Arrow Projectile")]
    [SerializeField] private int damage;
    [SerializeField] private float moveSpeed;

    [Header(" - Config")]
    [SerializeField] private GameObject light;
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private Rigidbody rigidbod;
    [SerializeField] private Transform impactEffect;

    private void OnEnable()
    {
        light.SetActive(true);
        particleSystem.Play();
        rigidbod.detectCollisions = true;
        rigidbod.velocity = transform.forward * moveSpeed;

    }


    //public void OnCollisionEnter(Collision collision) {
    //    rigidbod.isKinematic = true;
    //    if (collision.gameObject.CompareTag(Constants.Tag.PLAYER)) {
    //        collision.gameObject.GetComponent<IDamageable>().TakeDamage(new Damage(gameObject, damage, collision.transform.position));
    //    }

    //    impactEffect.SpawnObject(transform.position, Quaternion.identity);
    //    ReturnToPool();
    //}

    private void OnTriggerEnter(Collider other) {
        rigidbod.velocity = Vector3.zero;
        rigidbod.detectCollisions = false;

        //if (other.CompareTag(Constants.Tag.PLAYER)) {
        //    other.GetComponent<IDamageable>().TakeDamage(new Damage(damage));
        //}

        //impactEffect.SpawnObject(transform.position, Quaternion.identity);
        StartCoroutine(WaitParticles());
    }

    private IEnumerator WaitParticles() {
        light.SetActive(false);
        particleSystem.Stop();
        yield return new WaitForSeconds(2f);
        this.ReturnToPool();
    }
}
