using AdventureGame.Data;
using AdventureGame.Shared;
using AdventureGame.UI.Window;
using I2.Loc;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace AdventureGame.Gameplay
{
    public class ArenaController : GameController
    {
        [Title("Arena Controller")]
        [SerializeField] private EnemyGenerator enemyGenerator;
        [Space]
        [SerializeField] private GameEvent onSceneFadeOutEnd;
        [SerializeField] private Animator startCounterAnimator;

        [Header(" - Area HUD")]
        [SerializeField] private GameObject arenaHUD;
        [SerializeField] private LocalizedString go;
        [Space]
        [SerializeField] private TextMeshProUGUI timer;
        [SerializeField] private TextMeshProUGUI starterCounter;
        [SerializeField] private TextMeshProUGUI hudDefeatedEnemies;

        [Header(" - Texts")]
        [SerializeField] private SimpleWindow resultWindow;
        [SerializeField] private TextMeshProUGUI resultDefeatedEnemies;

        [Header(" - Death")]
        [SerializeField] private SimpleWindow deathWindow;
        [SerializeField] private TextMeshProUGUI deathDefeatedEnemies;

        private ArenaSettings Settings => GameSettings.Arena;

        private float time;
        private int secondsToStart;
        private bool timerRunning;

        private int defeatedEnemies;

        private const string Start = "Start";
        private const string Count = "Count";

        #region Init
        protected override void Awake()
        {
            base.Awake();
            DisableInputs();

            enemyGenerator.OnEnemyDeath += OnEnemyDie;
            onSceneFadeOutEnd += StartCounter;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            enemyGenerator.OnEnemyDeath -= OnEnemyDie;
            onSceneFadeOutEnd -= StartCounter;
        }
        #endregion

        #region Starting
        private void StartCounter()
        {
            time = Settings.RoundDuration;
            UpdateTimer();

            secondsToStart = Settings.SecondsToStart;
            starterCounter.text = secondsToStart.ToString();

            startCounterAnimator.Play(Count);
        }

        public void OnCounterTrigger()
        {
            secondsToStart--;
            if (secondsToStart > 0)
            {
                starterCounter.text = secondsToStart.ToString();
            }
            else
            {
                starterCounter.text = go;
                startCounterAnimator.Play(Start);

                enemyGenerator.enabled = true;
                timerRunning = true;
                EnableInputs();
            }
        }
        #endregion

        #region Playing
        private void FixedUpdate()
        {
            if (!timerRunning)
                return;

            time -= Time.fixedDeltaTime;

            if (time <= 0f)
            {
                time = 0f;
                timerRunning = false;
                Victory();
            }

            UpdateTimer();
        }

        private void UpdateTimer()
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(time);
            timer.text = $"{(int)timeSpan.TotalMinutes:D2}:{timeSpan:ss\\:fff}";
        }

        private void OnEnemyDie()
        {
            defeatedEnemies++;
            hudDefeatedEnemies.text = defeatedEnemies.ToString();
        }
        #endregion

        #region Defeat
        protected override void OnPlayerDeath(IPlayer player)
        {
            timerRunning = false;
            StartCoroutine(GameOverDelay());
        }

        private IEnumerator GameOverDelay()
        {
            yield return new WaitForSeconds(Settings.EndGameDelay);

            arenaHUD.SetActive(false);

            deathDefeatedEnemies.text = defeatedEnemies.ToString();
            deathWindow.RequestOpenWindow();
        }

        #endregion

        #region Victory
        private void Victory()
        {
            DisableInputs();
            enemyGenerator.OnEnemyDeath -= OnEnemyDie;

            enemyGenerator.enabled = false;
            enemyGenerator.KillAll();

            resultDefeatedEnemies.text = defeatedEnemies.ToString();

            StartCoroutine(VictoryDelay());
        }

        private IEnumerator VictoryDelay()
        {
            yield return new WaitForSeconds(Settings.EndGameDelay);

            arenaHUD.SetActive(false);
            resultWindow.RequestOpenWindow();
        }
        #endregion
    }
}