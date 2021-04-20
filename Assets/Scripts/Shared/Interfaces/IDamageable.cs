using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable {
    bool TakeDamage(Damage damage);
    bool IsDead { get; }
    // Retorna dados do gameobject?
}

public struct Damage {
    public GameObject sender;
    public Vector3 hitDirection;
    public int value;
    public Damage(GameObject sender, int value, Vector3 receiver) {
        this.sender = sender;
        hitDirection = (receiver - sender.transform.position).normalized;
        this.value = value;
    }
    public Damage(int value) {
        sender = null;
        hitDirection = Vector3.zero;
        this.value = value;
    }
}
