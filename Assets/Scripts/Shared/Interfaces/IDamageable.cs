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
    public Transform sender;
    public Vector3 hitDirection;
    public int value;
    public Damage(Transform sender, int value, Vector3 receiver) {
        this.sender = sender;
        hitDirection = (receiver - sender.position).normalized;
        this.value = value;
    }
    public Damage(int value) {
        sender = null;
        hitDirection = Vector3.zero;
        this.value = value;
    }
}
