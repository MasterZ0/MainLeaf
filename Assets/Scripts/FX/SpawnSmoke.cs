using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
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
    [SerializeField] private HDAdditionalLightData pointLight;
    [SerializeField] private VisualEffect visualEffect;

    public Action<Vector3> Callback { private get; set; }

    private LightState lightState;
    private float lightIntensity;

    private void Awake() {
        lightIntensity = pointLight.intensity;

    }
    protected override void OnEnablePooledObject() {
        pointLight.intensity = 0;
        StartCoroutine(Smoke());
    }

    private void FixedUpdate() {
        pointLight.intensity += lightState switch {
            LightState.Warming => Time.fixedDeltaTime * lightIntensity / warming,                // pointLight.intensity++
            LightState.Disappearing => -Time.fixedDeltaTime * lightIntensity / delayToDestroy ,   // pointLight.intensity--
            _ => 0
        };
    }
    IEnumerator Smoke() {
        visualEffect.Play();
        lightState = LightState.Warming;
        yield return new WaitForSeconds(warming);

        Callback?.Invoke(transform.position);
        lightState = LightState.Waiting;
        yield return new WaitForSeconds(disappears);

        visualEffect.Stop();
        lightState = LightState.Disappearing;
        yield return new WaitForSeconds(delayToDestroy);

        ReturnToPool();
    }

}
