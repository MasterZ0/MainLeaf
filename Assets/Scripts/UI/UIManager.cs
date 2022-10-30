
using AdventureGame.AppOptions;
using AdventureGame.Shared;
using AdventureGame.UI.Window;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AdventureGame.UI
{
    /// <summary>
    /// The new UI Manager
    /// </summary>
    public class UIManager : Singleton<UIManager>
    {
        [Title("UI Manager")]
        [SerializeField] private Popup popup;
        [SerializeField] private OptionsWindow options;

        [Title("Game Events")]
        [SerializeField] private GameEvent onOpenOptions;

        #region References
        public static Popup Popup => Instance.popup;
        #endregion

        public static void OpenOptions() => Instance.options.RequestOpenWindow();

        protected override void Awake()
        {
            base.Awake();
            options.Init();

            onOpenOptions += OpenOptions;
        }

        private void OnDestroy()
        {
            onOpenOptions -= OpenOptions;
        }
    }
}