using System;
using System.Collections;
using AdventureGame.Data;
using AdventureGame.ObjectPooling;
using Sirenix.OdinInspector;
using UnityEngine;
using AdventureGame.BattleSystem;
using AdventureGame.Effects;
using AdventureGame.Shake;

namespace AdventureGame.Player
{
    [Serializable]
    [FoldoutGroup("Visual Effects"), HideLabel, InlineProperty]
    public class PlayerVFX
    {
        [Title("Components")]
        //[SerializeField] private TrailGenerator trail;
        [SerializeField] private SkinnedMeshRenderer[] skinnedMeshes;

        [Title("Prefabs")]
        [SerializeField] private ParticleVFX dashFX;
        [SerializeField] private ParticleVFX jumpDustFX;
        [SerializeField] private ParticleVFX landingDustFX;
        [SerializeField] private ParticleVFX blood;
        [SerializeField] private ParticleVFX hpHealing;

        [Title("Shakes")]
        [SerializeField] private ShakeData damageShakeData;

        private Coroutine flashCoroutine;
        private Coroutine reddenCoroutine;

        #region Getters and Consts
        private float VisibleInjuredTime => Settings.VisibleInjuredTime;
        private float InvisibleInjuredTime => Settings.InvisibleInjuredTime;
        private float InjuryRedColorDuration => Settings.InjuryRedColorDuration;
        private float AlphaInvincible => Settings.AlphaInvisible;
        private PlayerVisualSettings Settings => controller.PlayerSettings.Visual;

        private PlayerController controller;

        private readonly int ColorID = Shader.PropertyToID("_Add_Color");
        private readonly int Alpha = Shader.PropertyToID("_Alpha");
        #endregion
        public void Init(PlayerController playerController)
        {
            controller = playerController;
            //trail.Init(Settings.TrailFrequency, Settings.TrailDuration);
        }

        public void SetActiveTrail(bool active)
        {
            //trail.enabled = active;
        }

        #region Injure
        public void Flashing(bool invincible)
        {
            StopCoroutine(flashCoroutine);
            if (invincible)
            {
                flashCoroutine = controller.StartCoroutine(Flash());
            }
            else
            {
                SetAlpha(1f);
            }
        }

        public void ReceiveDamage(Vector2 position, Quaternion rotation)
        {
            ParticleVFX particleVFX = ObjectPool.SpawnPooledObject(blood, position, rotation);
            particleVFX.SetColor(Settings.BloodColor);

            //Shaker.RequestShake(damageShakeData);

            StopCoroutine(reddenCoroutine);
            reddenCoroutine = controller.StartCoroutine(Redden());
        }

        private void StopCoroutine(Coroutine coroutine)
        {
            if (coroutine != null)
            {
                controller.StopCoroutine(coroutine);
            }
        }

        private IEnumerator Redden()
        {
            float redIntensity = 1f;
            Color color = Color.black;

            while (redIntensity > 0)
            {
                color.r = redIntensity;

                ApplyPropertiesToMaterial(ColorID, color);

                redIntensity -= Time.fixedDeltaTime / InjuryRedColorDuration;

                yield return new WaitForFixedUpdate();
            }

            ApplyPropertiesToMaterial(ColorID, Color.black);
        }

        private IEnumerator Flash()
        {
            while (true)
            {
                SetAlpha(AlphaInvincible);
                yield return new WaitForSeconds(InvisibleInjuredTime);

                SetAlpha(1f);
                yield return new WaitForSeconds(VisibleInjuredTime);
            }
        }

        private void SetAlpha(float alpha)
        {
            for (int i = 0; i < skinnedMeshes.Length; i++)
            {
                skinnedMeshes[i].material.SetFloat(Alpha, alpha);
            }
        }

        private void ApplyPropertiesToMaterial(int parameter, Color color)
        {
            for (int i = 0; i < skinnedMeshes.Length; i++)
            {
                skinnedMeshes[i].material.SetColor(parameter, color);
            }
        }
        #endregion

        #region Prefabs
        public void Dash() => SpawnFX(dashFX, controller.Pivot);
        public void Jump() => SpawnFX(jumpDustFX, controller.Pivot);
        public void Landing() => SpawnFX(landingDustFX, controller.Pivot);
        private void SpawnFX(Component effect, Transform orientation) => ObjectPool.SpawnPooledObject(effect, orientation.position, orientation.rotation);
        #endregion
    }
}
