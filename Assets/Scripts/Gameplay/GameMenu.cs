using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour {
    [Header("Game Menu")]

    [SerializeField] private Mask hideMask;
    [SerializeField] private GameObject gameMenuPanel;
    [SerializeField] private Button resumeBtn;

    private Controls controls;

    private bool paused;
    void Start()
    {
        controls = new Controls();
        controls.UI.Start.performed += ctx => OnPause();
        controls.Enable();
    }
    private void OnDisable() => controls.Disable();

    private void OnPause() {
        if (paused)
            return;

        paused = true;
        Time.timeScale = 0f;
        HUD.Instance.HideHud(true);
        gameMenuPanel.SetActive(true);
        StartCoroutine(DelaySelect());
    }

    IEnumerator DelaySelect() { //Delay necessario, porque o botão de pause é o mesmo de cancelar
        yield return new WaitForSecondsRealtime(.01f);
        resumeBtn.Select();
    }
    public void OnResume(bool confirm) {
        EventSystem.current.SetSelectedGameObject(null);

        HUD.Instance.HideHud(false);
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
