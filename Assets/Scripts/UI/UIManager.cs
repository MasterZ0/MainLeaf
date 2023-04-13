using AdventureGame.Inputs;
using AdventureGame.Shared;
using AdventureGame.UIElements;
using Z3.UIBuilder.Core;
using System;
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
            uiInputs.OnExtraA += OnExtraA;
            uiInputs.OnExtraB += OnExtraB;
        }

        public static void SetCursorVisible(bool visible) => Cursor.visible = visible;

        private void OnExtraA()
        {
            if (GetExtraUIHandle(out ICustomUIHandler handler))
            {
                handler.OnExtraA();
            }
        }

        private void OnExtraB()
        {
            if (GetExtraUIHandle(out ICustomUIHandler handler))
            {
                handler.OnExtraB();
            }
        }

        private bool GetExtraUIHandle(out ICustomUIHandler handler)
        {
            handler = null;
            GameObject selectedGameObject = EventSystem.current.currentSelectedGameObject;
            return selectedGameObject != null && selectedGameObject.TryGetComponent(out handler);
        }
    }
}