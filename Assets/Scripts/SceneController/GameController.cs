using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameController : MonoBehaviour {
    [Header("Game Controller")]

    [SerializeField] private float roundTime = 180;

    [Header(" - Config")]
    [SerializeField] private PlayerInputs player;
    public static Transform Player { get => Instance.player.transform; }
    
    public static GameController Instance { get; private set; }
    private void Awake() {
        Instance = this;
    }
    private void Start() {
        GameManager.Instance.SetTransitionCallback(OnTransitionOpen);
    }

    private void OnTransitionOpen() {
        HUD.Instance.Init(roundTime);
    }

    public void PlayerDeath() {
        player.SetControlsActive(false);

    }

    public void StartGame() {
        player.SetControlsActive(true);
    }
}
