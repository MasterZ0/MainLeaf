using AdventureGame.Data;
using AdventureGame.Shared;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AdventureGame.ApplicationManager
{
    /// <summary>
    /// Control the GameManager Scene
    /// </summary>
    public class GameManager : Singleton<GameManager>
    {
        #region Variables and Properties
        [Header("Game Manager")]
        [SerializeField] private Animator transitionAnimator;
        [SerializeField] private SceneLoader sceneLoader;
        [SerializeField] private GameSettings gameValues;

        [Header("Events")]
        [SerializeField] private GameEvent onFadeOutEnd;
        [SerializeField] private GameEvent onFadeInEnd;

        private GameScene? nextScene = null;

        private const string FadeIn = "FadeIn";
        private const string FadeOut = "FadeOut";

        public static event Action<bool> OnChanceFocus = delegate { };
        public static bool FocusOnGame { get; private set; }
        #endregion

        #region Initialization
        protected override void Awake()
        {
            Cursor.lockState = CursorLockMode.Confined;

            base.Awake();
            gameValues.Initialize();
            sceneLoader.LoadApplication(OnLoadFinish);
        }
        #endregion

        #region Focus
        private void OnApplicationFocus(bool hasFocus)
        {
            #if UNITY_EDITOR
            if (Application.isEditor)
                return;
            #endif

            FocusOnGame = hasFocus;
            OnChanceFocus(FocusOnGame);
        }
        #endregion

        #region Public Request
        public static void RequestReloadScene() => RequestLoadScene(null);
        public static void RequestLoadScene(GameScene? scene)
        {
            Instance.LoadScene(scene);
        }
        #endregion

        #region Events
        public void OnFadeInEnd()
        {
            onFadeInEnd.Invoke();
        }

        public void OnFadeOutEnd()
        {
            onFadeOutEnd.Invoke();

            if (nextScene.HasValue)
            {
                sceneLoader.LoadScene(nextScene.Value, OnLoadFinish);
            }
            else
            {
                sceneLoader.ReloadScene(OnLoadFinish);
            }

            nextScene = null;
            Time.timeScale = 1f;
        }
        #endregion

        #region Private Methods
        /// <summary> Fade Out Start </summary>
        private void LoadScene(GameScene? scene)
        {
            nextScene = scene;
            transitionAnimator.Play(FadeOut);
            EventSystem.current.SetSelectedGameObject(null);
        }

        /// <summary> Fade In Start </summary>
        private void OnLoadFinish()
        {
            transitionAnimator.Play(FadeIn);
        }
        #endregion
    }
}
