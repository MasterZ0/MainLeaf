using System;
using UnityEngine;

public class GameController : MonoBehaviour {

    [SerializeField] private int secondsToStart = 3;
    [SerializeField] private float roundTime = 180;

    public static event Action<GameState> OnChangeState = delegate { };
    private void Awake() {
        SetGameState(GameState.Initializing);
    }
    private void Start() {
        GameManager.Instance.SetTransitionCallback(OnTransitionOpen);
    }

    private void OnTransitionOpen() {
        HUD.SetupGameController(roundTime, secondsToStart);
    }

    public static void SetGameState(GameState gameState) {
        OnChangeState.Invoke(gameState);
    }
}
