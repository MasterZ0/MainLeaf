using System;
using UnityEngine;

public class GameController : MonoBehaviour {

    [SerializeField] private int secondsToStart = 3;
    [SerializeField] private float roundTime = 180;

    public static event Action<GameState> OnChangeState = delegate { };
    public static event Action<Enemy> OnEnemyDeath = delegate { };
    private void Start() {
        GameManager.MusicManager.ChangeMusic(Music.Gameplay);
        GameManager.Instance.SetTransitionCallback(OnTransitionOpen);
    }

    private void OnTransitionOpen() {
        SetGameState(GameState.Initializing);
        HUD.SetupGameController(roundTime, secondsToStart);
        GameMenu.SetActive(true);
    }

    public static void SetGameState(GameState gameState) {
        GameMenu.SetActive(gameState == GameState.Initializing || gameState == GameState.Playing);

        if (gameState == GameState.Win)
            OnEnemyDeath = null;    // Quando inimigos morrerem, não aumente a pontuação

        OnChangeState.Invoke(gameState);
    }

    public static void EnemyDeath(Enemy enemy) {
        OnEnemyDeath?.Invoke(enemy);
    }

    private void OnDestroy() {
        OnChangeState = null;
        OnEnemyDeath = null;
    }
}
