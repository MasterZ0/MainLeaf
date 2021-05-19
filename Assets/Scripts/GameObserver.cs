using UnityEngine;

public abstract class GameObserver : MonoBehaviour {
    protected GameState CurrentState { get => GameController.CurrentState; }
    protected virtual void Awake() {
        GameController.Playing += OnPlaying;
        GameController.Victory += OnVictory;
        GameController.GameOver += OnGameOver;
    }
    protected virtual void OnPlaying() { }
    protected virtual void OnVictory() { }
    protected virtual void OnGameOver() { }
    protected virtual void OnDestroy() {
        GameController.Playing -= OnPlaying;
        GameController.Victory -= OnVictory;
        GameController.GameOver -= OnGameOver;
    }
}
