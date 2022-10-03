
using AdventureGame.AppOptions;
using AdventureGame.Shared;
using Sirenix.OdinInspector;
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
        [SerializeField] private Options options;

        #region References
        public static Popup Popup => Instance.popup;
        public static Options Options => Instance.options;
        #endregion


        protected override void Awake()
        {
            base.Awake();
            options.Init();
        }
    }
}