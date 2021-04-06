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
    [SerializeField] private Slider lifeBar;
    [SerializeField] private Animator startCounterAnimator;

    [Header(" - Texts")]
    [SerializeField] private TextMeshProUGUI ammoCountText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI starterCounterText;
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private TextMeshProUGUI defeatedEnemiesText;


    public static HUD Instance { get; private set; }

    private float time;
    private int defeatedEnemies;
    private int totalPoints;
    private bool timePaused = true;

    private void Awake() {
        Instance = this;
    }
    private void FixedUpdate() {
        if (timePaused)
            return;

        UpdateTime();
    }

    public void Init(float roundTime) {
        time = roundTime;
        startCounterAnimator.Play(Constants.Anim.COUNT);
    }

    private void UpdateTime() {
        time += Time.fixedDeltaTime;
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        string displayTime = $"{Mathf.Floor((float)timeSpan.TotalHours).ToString("00")}:{timeSpan.ToString(@"mm\:ss")}";
        timerText.text = displayTime;

        if(time >= 3) {
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

    public void UpdateLife(float percentage) {
        lifeBar.value = percentage;
        if(percentage <= 0) {
            GameController.Instance.PlayerDeath();
        }
    }

    public void OnStarterCounterTrigger() {
        secondsToStart--;
        if(secondsToStart <= 0) {
            starterCounterText.text = "GO!";
            startCounterAnimator.Play(Constants.Anim.START);
        }
        else {
            starterCounterText.text = secondsToStart.ToString();
        }

    }
}
