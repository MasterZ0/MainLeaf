using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ArrowProjectile : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private Rigidbody rigidbod;
    CinemachineImpulseSource source;
    private void Awake() {
        rigidbod.centerOfMass = transform.position;
    }

    public void Fire()
    {
        rigidbod.AddForce(transform.forward * (100 * Random.Range(1.3f, 1.7f)), ForceMode.Impulse);
        source = GetComponent<CinemachineImpulseSource>();

        source.GenerateImpulse(Camera.main.transform.forward);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name != "Player")
        {
            rigidbod.isKinematic = true;
            StartCoroutine(Countdown());
        }
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }


}
