using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    [Header("Main Menu")]
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterSelection characterSelection;

    [Header(" - Cameras")]
    [SerializeField] private GameObject mainScreenCam;
    [SerializeField] private GameObject optionsCam;
    [SerializeField] private GameObject characterSelectionCam;
    [SerializeField] private GameObject[] charactersCam;


    private static string CHARACTER_INFO { get => "CharacterInfo"; }
    private static string MAIN_SCREEN { get => "MainScreen"; }

    private Options options;

    private GameObject currentCam;
    void Awake() {
        currentCam = mainScreenCam;
        //options = Options.Instance;

        GameManager.Instance.SetTransitionCallback(() => animator.SetBool(MAIN_SCREEN, true));
    }

    public void OnOpenMainScreen() {
        currentCam.SetActive(false);
        mainScreenCam.SetActive(true);
        currentCam = mainScreenCam;

        animator.SetBool(MAIN_SCREEN, true);
    }
    public void OnOpenOptions() {
        currentCam.SetActive(false);
        optionsCam.SetActive(true);
        currentCam = optionsCam;

        animator.SetBool(MAIN_SCREEN, false);
    }
    public void OnOpenCharacterSelection() {
        currentCam.SetActive(false);
        characterSelectionCam.SetActive(true);
        currentCam = characterSelectionCam;

        characterSelection.Active();

        animator.SetBool(MAIN_SCREEN, false);
        animator.SetBool(CHARACTER_INFO, false);
    }
    public void OnOpenCharacterInfo(int index) {

        currentCam.SetActive(false);
        charactersCam[index].SetActive(true);
        currentCam = charactersCam[index];

        animator.SetBool(CHARACTER_INFO, true);
    }
    public void OnPlay() {
        GameManager.Instance.LoadNewScene(SceneIndexes.Gameplay);
        PlayerPrefs.SetInt(Constants.PlayerPrefs.Int.SELECTED_CHARACTER, 0);
    }
    public void OnQuit() {
        Application.Quit();
    }
}
