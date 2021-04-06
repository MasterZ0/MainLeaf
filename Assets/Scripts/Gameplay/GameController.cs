using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    [Header("Game Controller")]

    [SerializeField] private float roundTime = 180;

    [Header(" - Config")]
    [SerializeField] private Transform player;
    public static Transform Player { get => Instance.player; }
    
    public static GameController Instance { get; private set; }

    private void Awake() {
        Instance = this;
        GameManager.Instance.SetTransitionCallback(OnTransitionOpen);
    }

    private void OnTransitionOpen() {
        HUD.Instance.Init(roundTime);
    }

    public void PlayerDeath() {

    }
}
