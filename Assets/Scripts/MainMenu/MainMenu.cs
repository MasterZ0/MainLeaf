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

        private const string MainScreen_FadeIn = "MainScreen_FadeIn";
        private const string CharacterInfo_FadeIn = "CharacterInfo_FadeIn";
        private const string Credits_FadeIn = "Credits_FadeIn";

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

            animator.Play(MainScreen_FadeIn);
        }
        public void OnOpenOptions()
        {
            EventSystem.current.SetSelectedGameObject(null);

            SwitchCamera(optionsCam);
            animator.Play(MainScreen_FadeIn);

            //onHideEnd = () => Options.OpenOption(OnOpenMainScreen);
        }

        public void SwitchCamera(GameObject newCamera) // New component?
        {
            currentCam.SetActive(false);
            currentCam = newCamera;
            currentCam.SetActive(true);
        }

        public void OnOpenCredits()
        {
            EventSystem.current.SetSelectedGameObject(null);

            SwitchCamera(creditsCam);

            animator.Play(Credits_FadeIn);
        }
        public void OnCloseCredits()
        {
            EventSystem.current.SetSelectedGameObject(null);

            SwitchCamera(mainScreenCam);
        }
        public void OnOpenCharacterSelection()
        {
            EventSystem.current.SetSelectedGameObject(null);

            SwitchCamera(characterSelectionCam);

            animator.Play(CharacterInfo_FadeIn);

            // After
            characterSelection.SetActive();
        }

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