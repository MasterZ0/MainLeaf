using AdventureGame.Shared;
using AdventureGame.UI.Window;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AdventureGame.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        [Title("Main Menu")]
        [SerializeField] private GameObject firstBtn;
        [SerializeField] private Animator animator;
        [SerializeField] private CharacterSelection characterSelection;

        [Header(" - Cameras")]
        [SerializeField] private GameObject mainScreenCam;
        [SerializeField] private GameObject optionsCam;
        [SerializeField] private GameObject creditsCam;
        [SerializeField] private GameObject characterSelectionCam;

        [Header(" - Events")]
        [SerializeField] private GameEvent onSceneFadeOutEnd;
        [SerializeField] private GameEvent onOpenOptions;

        private const string MainScreen_FadeIn = "MainScreen_FadeIn";
        private const string MainScreen_FadeOut = "MainScreen_FadeOut";
        private const string CharacterInfo_FadeIn = "CharacterInfo_FadeIn";
        private const string CharacterInfo_FadeOut = "CharacterInfo_FadeOut";
        private const string Credits_FadeIn = "Credits_FadeIn";
        private const string Credits_FadeOut = "Credits_FadeOut";

        private GameObject currentCam;
        private Action onFadeOutEnd;
        private GameObject nextBtn;

        private void Awake()
        {
            nextBtn = firstBtn;
            currentCam = mainScreenCam;
            onSceneFadeOutEnd += OnSceneFadeOutEnd;
            //GameManager.MusicManager.ChangeMusic(Music.MainMenu);
            //GameManager.Instance.SetTransitionCallback(() => animator.SetTrigger(CHANGE_SCREEN));
        }

        private void OnDestroy()
        {
            onSceneFadeOutEnd -= OnSceneFadeOutEnd;
        }

        public void OnSelectMainScreenButton()
        {
            EventSystem.current.SetSelectedGameObject(nextBtn);
        }

        private void OnSceneFadeOutEnd() => animator.Play(MainScreen_FadeIn);

        #region Button Events
        public void OnOpenOptions() // Fade Out MainScreen -> Auto Open Option
        {
            nextBtn = EventSystem.current.currentSelectedGameObject;
            SwitchCamera(optionsCam);

            onFadeOutEnd = onOpenOptions;
            WindowManager.OnCloseLastWindow += OnCloseOption;
        }

        private void OnCloseOption() // Fade In with Aut Select
        {
            WindowManager.OnCloseLastWindow -= OnCloseOption;
            SwitchCamera(mainScreenCam, MainScreen_FadeIn);
        }

        public void OnOpenCredits() // Fade Out MainScreen -> Play with Auto Select
        {
            nextBtn = EventSystem.current.currentSelectedGameObject;
            SwitchCamera(creditsCam);

            onFadeOutEnd = () => animator.Play(Credits_FadeIn);
        }

        public void OnCloseCredits() // Fade Out Credits -> Play with Auto Select
        {
            SwitchCamera(mainScreenCam, Credits_FadeOut);

            onFadeOutEnd = () => animator.Play(MainScreen_FadeIn);
        }

        public void OnOpenCharacterSelection() // Fade Out MainScreen -> Select a character
        {
            nextBtn = EventSystem.current.currentSelectedGameObject;
            SwitchCamera(characterSelectionCam);

            onFadeOutEnd = () => characterSelection.SelectCharacter();
        }

        public void OnCloseCharacterSelection() // Fade In with Aut Select
        {
            SwitchCamera(mainScreenCam, MainScreen_FadeIn);
        }

        public void OnCloseCharacterInfo() // Fade Out CharacterInfo -> Select a character
        {
            SwitchCamera(characterSelectionCam, CharacterInfo_FadeOut);

            onFadeOutEnd = () => characterSelection.SelectCharacter();
        }
        public void OnQuit()
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }
        #endregion

        public void OnFadeOutEnd() => onFadeOutEnd();

        public void ShowCharacter(GameObject characterCam) // Fade In CharacterInfo with Auto Select
        {
            SwitchCamera(characterCam, CharacterInfo_FadeIn);
        }

        private void SwitchCamera(GameObject nextCamera, string animationState = MainScreen_FadeOut)
        {
            currentCam.SetActive(false);
            currentCam = nextCamera;
            currentCam.SetActive(true);

            EventSystem.current.SetSelectedGameObject(null);
            animator.Play(animationState);
        }
    }
}