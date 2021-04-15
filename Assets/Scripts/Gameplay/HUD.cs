using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
    [Header("HUD")]
    [SerializeField] private int secondsToStart = 3;

    [Header(" - Texts")]
    [SerializeField] private Mask hideMask;
    [SerializeField] private Slider lifeBar;
    [SerializeField] private Animator startCounterAnimator;

    [Header(" - Texts")]
    [SerializeField] private TextMeshProUGUI ammoCountText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI starterCounterText;
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private TextMeshProUGUI defeatedEnemiesText;


    //public static event Action addPoints2; // event -> fora da class, pode somente se escrever
    //public static Func<int, string> func; // action with return
    //public delegate int ExempleDelagate(int points); // Definição
    //public static ExempleDelagate addPoints; // Instancia, pode ser chamada e alterada
    public static HUD Instance { get; private set; }

    private float time;
    private int defeatedEnemies;
    private int totalPoints;
    private bool timePaused = true;

    private void Awake() {
        Instance = this;
    }
    #region Init
    public void StartGame(float roundTime) {
        time = roundTime;
        starterCounterText.text = secondsToStart.ToString();
        startCounterAnimator.Play(Constants.Anim.COUNT);
    }

    public void PlayerSetup(int maxLife) {
        // Influenciar tamanho da barra?
    }

    public void OnPlayerDeath() {
        throw new NotImplementedException();
    }

    public void UpdateLife(float percentage) {
        lifeBar.value = percentage;
        if (percentage <= 0) {
            GameController.Instance.OnPlayerDeath();
        }
    }

    #endregion

    public void HideHud(bool active) => hideMask.enabled = active;

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
            EndGame();
        }
    }

    private void EndGame() {
        pointsText.text = totalPoints.ToString();
        defeatedEnemiesText.text = defeatedEnemies.ToString();
    }
    public void AddPoints(int enemyPoints) {
        defeatedEnemies++;
        totalPoints += enemyPoints;
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
            GameController.Instance.StartGame();
        }

    }

    public void UpdateAmmoCount(int ammo) {
        ammoCountText.text = $"{ammo} x";
    }
}
