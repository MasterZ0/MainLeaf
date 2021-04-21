using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour {
    [Header("Game Menu")]

    [SerializeField] private Mask hideMask;
    [SerializeField] private GameObject gameMenuPanel;
    [SerializeField] private Button resumeBtn;

    private static Controls controls;
    public static event Action<bool> OnPause;

    private bool paused;
    void Awake() {
        controls = new Controls();
        controls.UI.Start.performed += ctx => OnPressPause();
    }
    private void OnDisable() => controls.Disable();
    public static void SetActive(bool active) {
        if (active) {
            controls.Enable();
        }
        else {
            controls.Disable();
        }
    }

    private void OnPressPause() {
        if (paused)
            return;

        paused = true;
        Time.timeScale = 0f;
        OnPause.Invoke(true);
        gameMenuPanel.SetActive(true);
        StartCoroutine(DelaySelect());
    }

    IEnumerator DelaySelect() { //Delay necessario, porque o botão de pause é o mesmo de cancelar
        yield return new WaitForSecondsRealtime(.01f);
        resumeBtn.Select();
    }
    public void OnResume(bool confirm) {
        EventSystem.current.SetSelectedGameObject(null);

        OnPause.Invoke(false);
        gameMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }

    public void OnOpenOptions() {
        hideMask.enabled = true;
        gameMenuPanel.SetActive(false);
        Options.OpenOption(OnCloseOptions);
    }
    public void OnCloseOptions() {
        hideMask.enabled = false;
        gameMenuPanel.SetActive(true);
        resumeBtn.Select();
    }
    public void OnQuit() {
        hideMask.enabled = true;
        GameManager.Instance.LoadNewScene(SceneIndexes.MainMenu);
    }
}
