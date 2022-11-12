using AdventureGame.Inputs;
using AdventureGame.Shared;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

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
        public static Popup Popup => Instance.popup;
        #endregion

        private UIInputs uiInputs;

        protected override void Awake()
        {
            base.Awake();
            options.Init();
            uiInputs = new UIInputs();
            uiInputs.OnExtra += OnExtraButton;
        }

        private void OnExtraButton()
        {
            GameObject selectedGameObject = EventSystem.current.currentSelectedGameObject;
            if (selectedGameObject != null && selectedGameObject.TryGetComponent(out IExtraButtonHandler handler))
            {
                handler.OnExtraButton();
            }
        }
    }
}