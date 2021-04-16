using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoActionOnTrigger : MonoBehaviour
{
    public Action test;
    public UnityEvent onEnterTrigger;
    public UnityEvent onExitTrigger;

    private void OnTriggerEnter(Collider other) {
        onEnterTrigger.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision) {
        onEnterTrigger.Invoke();
    }
}
