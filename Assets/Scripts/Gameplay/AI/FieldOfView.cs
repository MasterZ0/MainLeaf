using System;
using System.Collections;
using UnityEngine;

public class FieldOfView : MonoBehaviour {
    [Header("FieldOfView")]
    [SerializeField] private float viewRadius;
    [SerializeField] [Range(0, 360)] private float viewAngle;

    [Header(" - Config")]
    [SerializeField] private Transform eye;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstacleMask;

    public float ViewRadius { get => viewRadius;  }

    public bool HasTarget() {
        // target está vivo?

        return false;
    }

    public float ViewAngle { get => viewAngle;  }
        
    private const float delayToSearch = .2f;
    private Action<Transform> findedCallback;

    public void FindTarget(Action<Transform> callback) {
        findedCallback = callback;
        StartCoroutine(FindTargetWithDelay());
    }

    private IEnumerator FindTargetWithDelay() {
        bool successful = false;
        while (!successful) {
            successful = FindVisibleTargets();
            yield return new WaitForSeconds(delayToSearch);
        }
    }
    private bool FindVisibleTargets() {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(eye.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++) {
            Transform target = targetsInViewRadius[i].transform;

            Vector3 targetDirection = (target.position - eye.position).normalized;
            if (Vector3.Angle(eye.forward, targetDirection) < viewAngle / 2) { // Está dentro do angulo de visão?
                float distToTarget = Vector3.Distance(eye.position, target.position);

                if (!Physics.Raycast(eye.position, targetDirection, distToTarget, obstacleMask)) { // Se não houver nenhum obstaculo
                    findedCallback(target);
                    return true;
                }
            }
        }
        return false;
    }

}
