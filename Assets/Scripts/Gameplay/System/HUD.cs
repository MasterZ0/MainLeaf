using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class HUD : MonoBehaviour {

    [Header(" - Config")]
    [SerializeField] private Mask hideMask;
    [SerializeField] private Slider lifeBar;
    [SerializeField] private Animator startCounterAnimator;

    [Header(" - Screens")]
    [SerializeField] private GameObject gameScreen;
    [SerializeField] private GameObject resultScreen;
    [SerializeField] private GameObject deathScreen;

    [SerializeField] private Button replayBtn;
    [SerializeField] private Button playAgainBtn;

    [Header(" - Texts")]
    [SerializeField] private TextMeshProUGUI ammoCountText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI starterCounterText;
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private TextMeshProUGUI defeatedEnemiesText;

    private static HUD Instance;

    private float time;
    private int secondsToStart;
    private int defeatedEnemies;
    private int totalPoints;
    private bool timePaused = true;

    #region Init
    private void Awake() {
        Instance = this;
        Enemy.OnEnemyDeath += UpdateScore;
        GameMenu.OnPause += HideHud;
        GameController.OnChangeState += OnChangeGameState;
    }
    public static void SetupGameController(float roundTime, int secondsToStart) {
        Instance.StartGame(roundTime, secondsToStart);
    }
    public static void SetupPlayer(int maxLife, ref Action<float> updateLife, ref Action<int> updateAmmoCount) {
        Instance.SetupPlayerd(maxLife, ref updateLife, ref updateAmmoCount);
    }
    private void SetupPlayerd(int maxLife, ref Action<float> updateLife, ref Action<int> updateAmmoCount) {
        // max life influencia no tamanho da barra
        updateLife += UpdateLife;
        updateAmmoCount += UpdateAmmoCount;
    }

    private void StartGame(float roundTime, int _secondsToStart) {
        time = roundTime;
        secondsToStart = _secondsToStart;
        starterCounterText.text = secondsToStart.ToString();
        startCounterAnimator.Play(Constants.Anim.COUNT);
    }

    private void HideHud(bool hide) => hideMask.enabled = hide;

    
    private void OnChangeGameState(GameState gameState) {
        if (gameState == GameState.PlayerDied) {
            DeathScreen();
        }
    }
    private void UpdateLife(float percentage) {
        lifeBar.value = percentage;
    }

    #endregion

    private void FixedUpdate() {
        if (timePaused)
            return;

        UpdateTime();
    }

    private void UpdateTime() {
        time -= Time.fixedDeltaTime;
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        string displayTime = $"{Mathf.Floor((float)timeSpan.TotalHours).ToString("00")}:{timeSpan.ToString(@"mm\:ss")}";
        timerText.text = displayTime;

        if(time <= 0) {
            timePaused = true;
            Victory();
        }
    }

    private void Victory() {
        GameController.SetGameState(GameState.Win);

        pointsText.text = totalPoints.ToString();
        defeatedEnemiesText.text = defeatedEnemies.ToString();

        gameScreen.SetActive(false);
        resultScreen.SetActive(true);
        playAgainBtn.Select();
        GameMenu.SetActive(false);
    }

    private void DeathScreen() {
        gameScreen.SetActive(false);
        deathScreen.SetActive(true);
        replayBtn.Select();
        GameMenu.SetActive(false);
    }

    private void UpdateScore(Enemy enemy) {
        defeatedEnemies++;
        totalPoints += enemy.EnemyAttributes.points;
    }

    private void UpdateAmmoCount(int ammo) {
        ammoCountText.text = $"{ammo} x";
    }

    public void OnStarterCounterTrigger() {
        secondsToStart--;
        if(secondsToStart > 0) {
            starterCounterText.text = secondsToStart.ToString();
        }
        else { 
            starterCounterText.text = "GO!";
            startCounterAnimator.Play(Constants.Anim.START);
            timePaused = false;
            GameController.SetGameState(GameState.Playing);
        }
    }
    private void OnDestroy() {
        Enemy.OnEnemyDeath -= UpdateScore;
        GameMenu.OnPause -= HideHud;
        GameController.OnChangeState -= OnChangeGameState;
    }
}
