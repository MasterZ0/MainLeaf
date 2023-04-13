using AdventureGame.ApplicationManager;
using AdventureGame.Audio;
using AdventureGame.Inputs;
using AdventureGame.UI;
using AdventureGame.UI.Window;
using I2.Loc;
using Z3.UIBuilder.Core;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AdventureGame.Gameplay
{
    /// <summary>
    /// Open the pause menu
    /// </summary>
    public class PauseMenu : MonoBehaviour, IControlInput
    {
        [Title("Pause Menu")]
        [SerializeField] private LocalizedString mainMenuQuestion;
        [SerializeField] private IWindow mainScreen;

        [Title("SFX")]
        [SerializeField] private SoundReference openMenuSound;
        [SerializeField] private SoundReference closeMenuSound;

        [Title("Main Menu")]
        [SerializeField] private Button mainMenuBtn;
        [SerializeField] private UnityEvent onMainMenu;

        public static event Action<bool> OnPause = delegate { };

        private UIInputs uiInputs;
        private bool paused;

        private bool CanPause => GameplayReferences.InputActive;

        private void Awake()
        {
            uiInputs = new UIInputs();
            uiInputs.OnPause += OnPressPause;
            GameManager.OnChanceFocus += OnChanceFocus;
        }

        private void OnDestroy()
        {
            GameplayReferences.SetActivePlayerInput(true, this);
            uiInputs.Dispose();
            GameManager.OnChanceFocus -= OnChanceFocus;

            WindowManager.CloseAllWindows();
        }

        #region Pause
        private void OnChanceFocus(bool focusOnGame)
        {
            if (!focusOnGame && !paused && CanPause)
            {
                OnPressPause();
            }
        }

        private void OnPressPause()
        {
            if (paused)
            {
                if (mainScreen.IsCurrentOpen())
                {
                    OnResume();
                }

                return;
            }

            if (!CanPause)
                return;

            PauseGame(true);
        }

        public void OnResume()
        {
            if (!paused) // The keyboard can call you twice at the same time, due to Esc and Cancel allow you to pause the game
                return;

            PauseGame(false);
        }

        private void PauseGame(bool pause)
        {
            paused = pause;

            if (pause)
            {
                mainScreen.RequestOpenWindow();
                openMenuSound.PlaySound();
                AudioManager.PauseSounds();
                UISounds.IgnoreNext();
            }
            else // Resume
            {
                mainScreen.TryCloseWindow();
                closeMenuSound.PlaySound();
                AudioManager.UnpauseSounds();
            }

            Time.timeScale = pause ? 0f : 1f;
            GameplayReferences.SetActivePlayerInput(!pause, this);
            OnPause(pause);
        }
        #endregion

        public void OnMainMenu()
        {
            uiInputs.SetActive(false);
            UIManager.Popup.RequestQuestion(mainMenuQuestion, OnAnswer, false);

            void OnAnswer(bool result)
            {
                if (result)
                {
                    onMainMenu.Invoke();
                }
                else
                {
                    uiInputs.SetActive(true);
                    mainMenuBtn.Select();
                }
            }
        }
    }
}