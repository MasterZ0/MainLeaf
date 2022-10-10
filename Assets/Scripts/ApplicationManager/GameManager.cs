using AdventureGame.Data;
using AdventureGame.Shared;
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
        #endregion

        #region Initialization
        protected override void Awake()
        {
            
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;

            #if !UNITY_EDITOR

            bool successful = StartDRM();
            if (!successful)
            {
                Application.Quit();
                return;
            }
            #endif

            base.Awake();
            gameValues.Initialize();
            sceneLoader.LoadApplication(OnLoadFinish);
        }
        #endregion

        private bool StartDRM() => true; // TODO

        #region Public Request
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
