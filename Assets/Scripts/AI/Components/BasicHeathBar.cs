using AdventureGame.BattleSystem;
using AdventureGame.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace AdventureGame.AI
{
    public class BasicHeathBar : MonoBehaviour
    {
        [SerializeField] private Enemy damageable; // IDamageable
        [SerializeField] private GameObject bar;
        [SerializeField] private Slider healthBar;
        [SerializeField] private Slider damageDealtBar;

        private GeneralSettings Settings => GameSettings.General;

        private Coroutine coroutine;
        private float previousHeathPercentage;

        private float CurrentHeathPercentage => (float)damageable.CurrentHealth / damageable.MaxHealth;

        private void OnEnable()
        {
            damageable.OnTakeDamage += OnTakeDamage;
            bar.SetActive(false);
        }

        private void OnDisable()
        {
            damageable.OnTakeDamage -= OnTakeDamage;
        }

        private void Update()
        {
            if (previousHeathPercentage > CurrentHeathPercentage)
            {
                previousHeathPercentage -= Settings.HeathBarReductionDamageDealt * Time.deltaTime;
                damageDealtBar.value = previousHeathPercentage;

                if (previousHeathPercentage <= CurrentHeathPercentage)
                {
                    coroutine = StartCoroutine(ShowHeathBar());
                }
            }
        }

        private void OnTakeDamage(DamageInfo damageInfo)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }

            float damageDealtPercentage = (float)damageInfo.EffectiveDamage / damageable.MaxHealth;
            previousHeathPercentage = CurrentHeathPercentage + damageDealtPercentage;

            damageDealtBar.value = previousHeathPercentage;
            healthBar.value = CurrentHeathPercentage;

            bar.SetActive(true);
        }

        private IEnumerator ShowHeathBar()
        {
            yield return new WaitForSeconds(Settings.HeathBarLifetime);
            bar.SetActive(false);
        }
    }
}
