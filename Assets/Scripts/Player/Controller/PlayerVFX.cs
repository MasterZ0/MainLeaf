using System;
using System.Collections;
using AdventureGame.Data;
using AdventureGame.ObjectPooling;
using Z3.UIBuilder.Core;
using UnityEngine;
using AdventureGame.BattleSystem;
using AdventureGame.Effects;
using AdventureGame.Shake;
using RootMotion.Demos;

namespace AdventureGame.Player
{
    [Serializable]
    //[FoldoutGroup("Visual Effects"), HideLabel, InlineProperty]
    public class PlayerVFX : PlayerClass
    {
        [Title("VFX")]
        //[SerializeField] private TrailGenerator trail;
        [SerializeField] private SkinnedMeshRenderer[] skinnedMeshes;

        [Title("Prefabs")]
        [SerializeField] private ParticleVFX dashFX;
        [SerializeField] private ParticleVFX jumpDustFX;
        [SerializeField] private ParticleVFX landingDustFX;
        [SerializeField] private ParticleVFX blood;
        [SerializeField] private ParticleVFX hpHealing;

        [Title("Shakes")]
        [SerializeField] private ShakeData shootShake;
        [SerializeField] private ShakeData damageShakeData;

        private Coroutine flashCoroutine;
        private Coroutine reddenCoroutine;

        #region Getters and Consts
        private float VisibleInjuredTime => Settings.VisibleInjuredTime;
        private float InvisibleInjuredTime => Settings.InvisibleInjuredTime;
        private float InjuryRedColorDuration => Settings.InjuryRedColorDuration;
        private float AlphaInvincible => Settings.AlphaInvisible;
        private PlayerVisualSettings Settings => Controller.PlayerSettings.Visual;

        private readonly int ColorID = Shader.PropertyToID("_BaseColor");
        private readonly int Alpha = Shader.PropertyToID("_BaseColor");
        #endregion

        #region VFX
        public void ShootArrow() => Shaker.RequestShake(shootShake);
        #endregion

        #region Injure
        public void Flashing(bool invincible)
        {
            StopCoroutine(flashCoroutine);
            if (invincible)
            {
                flashCoroutine = Controller.StartCoroutine(Flash());
            }
            else
            {
                SetAlpha(1f);
            }
        }

        public void ReceiveDamage(DamageInfo damageInfo)
        {
            Shaker.RequestShake(damageShakeData);

            StopCoroutine(reddenCoroutine);
            reddenCoroutine = Controller.StartCoroutine(Redden());

            if (!damageInfo.Damage.ShowHitParticle)
                return;

            GetContacts(damageInfo, out Vector3 position, out Quaternion rotation);

            ParticleVFX particleVFX = ObjectPool.SpawnPooledObject(blood, position, rotation);
            particleVFX.SetColor(Settings.BloodColor);
        }

        private void StopCoroutine(Coroutine coroutine)
        {
            if (coroutine != null)
            {
                Controller.StopCoroutine(coroutine);
            }
        }

        private void GetContacts(DamageInfo damageInfo, out Vector3 contactPoint, out Quaternion contactRotation)
        {
            contactPoint = damageInfo.Damage.ContactPoint ?? Controller.Center.position;
            contactRotation = damageInfo.Damage.ContactRotation ?? Controller.Center.rotation;
        }

        private IEnumerator Redden()
        {
            float redIntensity = 0f;
            Color color = Color.red;

            while (redIntensity < 1)
            {
                color = Color.Lerp(Color.red, Color.white, redIntensity);
                ApplyPropertiesToMaterial(ColorID, color);

                redIntensity += Time.fixedDeltaTime / InjuryRedColorDuration;
                yield return new WaitForFixedUpdate();
            }

            ApplyPropertiesToMaterial(ColorID, Color.white);
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
        public void Dash() => SpawnFX(dashFX, Controller.Pivot);
        public void Jump() => SpawnFX(jumpDustFX, Controller.Pivot);
        public void Landing() => SpawnFX(landingDustFX, Controller.Pivot);
        private void SpawnFX(Component effect, Transform orientation) => ObjectPool.SpawnPooledObject(effect, orientation.position, orientation.rotation);
        #endregion
    }
}
