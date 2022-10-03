using AdventureGame.ApplicationManager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AdventureGame.MainMenu
{
    public class MainMenu : MonoBehaviour
    {

        [Header("Main Menu")]
        [SerializeField] private Animator animator;
        [SerializeField] private CharacterSelection characterSelection;

        [Header(" - Cameras")]
        [SerializeField] private GameObject mainScreenCam;
        [SerializeField] private GameObject optionsCam;
        [SerializeField] private GameObject creditsCam;
        [SerializeField] private GameObject characterSelectionCam;
        [SerializeField] private GameObject[] charactersCam;

        private Action onHideEnd;
        private static string SELECTION { get => "Selection"; }
        private static string CHANGE_SCREEN { get => "ChangeScreen"; }

        private GameObject currentCam;
        void Start()
        {
            currentCam = mainScreenCam;
            //GameManager.MusicManager.ChangeMusic(Music.MainMenu);
            //GameManager.Instance.SetTransitionCallback(() => animator.SetTrigger(CHANGE_SCREEN));
        }

        public void OnOpenMainScreen()
        {
            EventSystem.current.SetSelectedGameObject(null);

            currentCam.SetActive(false);
            mainScreenCam.SetActive(true);
            currentCam = mainScreenCam;

            animator.SetInteger(SELECTION, 0);
            animator.SetTrigger(CHANGE_SCREEN);
        }
        public void OnOpenOptions()
        {
            EventSystem.current.SetSelectedGameObject(null);

            currentCam.SetActive(false);
            optionsCam.SetActive(true);
            currentCam = optionsCam;

            animator.SetInteger(SELECTION, -1);
            //onHideEnd = () => Options.OpenOption(OnOpenMainScreen);
        }
        public void OnOpenCredits()
        {
            EventSystem.current.SetSelectedGameObject(null);

            currentCam.SetActive(false);
            creditsCam.SetActive(true);
            currentCam = creditsCam;

            animator.SetInteger(SELECTION, 1);
            onHideEnd = () => animator.SetTrigger(CHANGE_SCREEN);
        }
        public void OnCloseCredits()
        {
            EventSystem.current.SetSelectedGameObject(null);

            currentCam.SetActive(false);
            mainScreenCam.SetActive(true);
            currentCam = mainScreenCam;

            animator.SetInteger(SELECTION, 0);
            onHideEnd = () => animator.SetTrigger(CHANGE_SCREEN);
        }
        public void OnOpenCharacterSelection(bool submit)
        {
            EventSystem.current.SetSelectedGameObject(null);

            currentCam.SetActive(false);
            characterSelectionCam.SetActive(true);
            currentCam = characterSelectionCam;

            animator.SetInteger(SELECTION, -1);

            onHideEnd = characterSelection.SetActive;
        }
        public void OnOpenCharacterInfo(int index)
        {
            currentCam.SetActive(false);
            charactersCam[index].SetActive(true);
            currentCam = charactersCam[index];

            animator.SetInteger(SELECTION, 2);
            animator.SetTrigger(CHANGE_SCREEN);
        }
        public void OnHideEnd() => onHideEnd();

        public void OnPlay()
        {
            //GameManager.Instance.LoadNewScene(SceneIndexes.Gameplay);
            //PlayerPrefs.SetInt(Constants.PlayerPrefs.Int.SELECTED_CHARACTER, 0);
        }
        public void OnQuit()
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }
    }
}