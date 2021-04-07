using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject firstScreen;
    [SerializeField] private Button firstButton;

    private GameObject currentScreen;
    private GameObject nextScreen;
    private Button nextButton;
    void Awake()
    {
        currentScreen = firstScreen;
        nextButton = firstButton;
        currentScreen.SetActive(true);

        GameManager.Instance.SetTransitionCallback(OnTransitionOpenEnd);
    }

    public void OnPlay() {
        GameManager.Instance.LoadNewScene(Constants.Scene.GAMEPLAY);
    }
    public void OnChangeScreen(GameObject screen) {         // Altera a tela
        EventSystem.current.SetSelectedGameObject(null);
        nextScreen = screen;

        GameManager.Instance.CloseTransition(OnTransitionCloseEnd);
    }
    public void OnNextButton(Button button) {
        nextButton = button;
    }

    public void OnTransitionOpenEnd() {   // Seleciona o proximo botão quando animação finaliza
        nextButton?.Select();
        nextButton = null;
    }

    public void OnTransitionCloseEnd() {  // Troca de tela
        currentScreen.SetActive(false);
        nextScreen.SetActive(true);
        currentScreen = nextScreen;

        GameManager.Instance.OpenTransition();
    }
    public void OnQuit() {
        Application.Quit();
    }
}
