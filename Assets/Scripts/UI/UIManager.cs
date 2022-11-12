using AdventureGame.Shared;
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

        #region References
        public static OptionsWindow Options => Instance.options;
        public static Popup Popup => Instance.popup;
        #endregion

        protected override void Awake()
        {
            base.Awake();
            options.Init();
        }
    }
}