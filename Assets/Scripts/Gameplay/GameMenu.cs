using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour {
    [SerializeField] private GameObject panel;
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
        panel.SetActive(true);
        StartCoroutine(DelaySelect());
    }

    IEnumerator DelaySelect() {
        yield return new WaitForSecondsRealtime(.01f);
        resumeBtn.Select();
    }
    public void OnResume(bool confirm) {
        paused = false;
        Time.timeScale = 1f;
        panel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void OnOptions() {

    }

    public void OnQuit() {
        GameManager.Instance.LoadNewScene(Constants.Scene.MAIN_MENU);
    }
}
