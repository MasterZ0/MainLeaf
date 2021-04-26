using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialFX : PooledObject {

    [Header("Special FX")]
    [SerializeField] private ParticleSystem particleSystem;

    protected override void OnEnablePooledObject() {
        StartCoroutine(WaitToFinish());
    }

    IEnumerator WaitToFinish() {
        yield return new WaitUntil(() => !particleSystem.isPlaying);
        ReturnToPool();
    }
}
