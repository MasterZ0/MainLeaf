using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LookAt : MonoBehaviour {
    [SerializeField] private bool once;

    [SerializeField] private bool x;
    [SerializeField] private bool y;
    [SerializeField] private bool z;
    [SerializeField] private Vector3 rotationOffset;

    [SerializeField] private Transform lookPoint;

    private void OnValidate() {
        if(lookPoint != null)
            SetDirection();
    }

    void Start() {
        if (once) {
            SetDirection();
            Destroy(this);
        }
    }

    private void SetDirection() {
        //Vector3 direction = lookPoint.position - transform.position;

        //if (!x) {
        //    direction.x = transform.position.x;
        //}
        //if (!y) {
        //    direction.y = transform.position.y;
        //}
        //if (!z) {
        //    direction.z = transform.position.z;
        //}
        transform.LookAt(lookPoint.position);
        transform.eulerAngles = transform.eulerAngles + rotationOffset;
    }

    void Update()
    {
        SetDirection();
    }
}
