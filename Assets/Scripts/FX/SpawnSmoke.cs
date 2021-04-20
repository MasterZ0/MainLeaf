using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SpawnSmoke : PooledObject {
    private enum LightState {
        Warming,
        Waiting,
        Disappearing
    }

    [Header("SpawnSmoke")]
    [SerializeField] private float warming = 2;
    [SerializeField] private float disappears = 3;
    [SerializeField] private float delayToDestroy = 2;
    [SerializeField] private Light pointLight;
    [SerializeField] private VisualEffect visualEffect;

    public Action<Vector3> Callback { private get; set; }

    private LightState lightState;
    private float lightIntensity;

    private void Awake() {
        lightIntensity = pointLight.intensity;
        pointLight.intensity = 0;

    }
    protected override void OnEnablePooledObject() {
        pointLight.intensity = 0;
        StartCoroutine(Smoke());
    }

    private void Update() {
        pointLight.intensity += lightState switch {
            LightState.Warming => Time.deltaTime / warming * lightIntensity,                // pointLight.intensity++
            LightState.Disappearing => -Time.deltaTime / delayToDestroy * lightIntensity,   // pointLight.intensity--
            _ => 0
        };
    }
    IEnumerator Smoke() {
        visualEffect.Play();
        lightState = LightState.Warming;
        yield return new WaitForSeconds(warming);

        Callback(transform.position);
        lightState = LightState.Waiting;
        yield return new WaitForSeconds(disappears);

        visualEffect.Stop();
        lightState = LightState.Disappearing;
        yield return new WaitForSeconds(delayToDestroy);

        ReturnToPool();
    }

}
