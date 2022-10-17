using AdventureGame.ObjectPooling;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.VFX;

namespace AdventureGame.Effects
{
    public class SpawnSmoke : MonoBehaviour
    {
        private enum LightState
        {
            Warming,
            Waiting,
            Disappearing
        }

        [Title("Spawn Smoke")]
        [SerializeField] private HDAdditionalLightData pointLight;
        [SerializeField] private VisualEffect visualEffect;

        private Action<Vector3> warmedCallback;

        private LightState lightState;
        private float lightIntensity;

        [Header("Test Only")]
        [ShowInInspector]
        private float warmingDuration = 2;
        [ShowInInspector]
        private float disappearsDuration = 3;
        [ShowInInspector]
        private float delayToDestroy = 2; // Particle lifetime

        private void Awake()
        {
            lightIntensity = pointLight.intensity;
        }

        private void OnEnable()
        {
            pointLight.intensity = 0;

            StartCoroutine(Smoke());
        }

        public void Init(Action<Vector3> warmedCallback, float warmingDuration, float disappearsDuration, float delayToDestroy)
        {
            this.warmedCallback = warmedCallback;
            this.warmingDuration = warmingDuration;
            this.disappearsDuration = disappearsDuration;
            this.delayToDestroy = delayToDestroy;
        }

        private void FixedUpdate()
        {
            pointLight.intensity += lightState switch
            {
                LightState.Warming => Time.fixedDeltaTime * lightIntensity / warmingDuration,                // pointLight.intensity++
                LightState.Disappearing => -Time.fixedDeltaTime * lightIntensity / delayToDestroy,   // pointLight.intensity--
                _ => 0
            };
        }
        private IEnumerator Smoke()
        {
            visualEffect.Play();
            lightState = LightState.Warming;
            yield return new WaitForSeconds(warmingDuration);

            warmedCallback?.Invoke(transform.position);
            lightState = LightState.Waiting;
            yield return new WaitForSeconds(disappearsDuration);

            visualEffect.Stop();
            lightState = LightState.Disappearing;
            yield return new WaitUntil(() => visualEffect.aliveParticleCount == 0);

            this.ReturnToPool();
        }
    }
}