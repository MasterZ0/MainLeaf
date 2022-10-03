using AdventureGame.Data;
using AdventureGame.Inputs;
using AdventureGame.Persistence;
using AdventureGame.UIElements;
using I2.Loc;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AdventureGame.AppOptions
{
    public class ControlsOptions : InputRebinderManager
    {
        [Title("Controls Settings")]
        [SerializeField] private Navigator deviceNavigator;
        [SerializeField] private GameObject keyboardPanel;
        [SerializeField] private GameObject gamepadPanel;
        [SerializeField] private Button keyboardFirstBtn;
        [SerializeField] private Button gamepadFirstBtn;

        [Header("Texts")]
        [SerializeField] private LocalizedString pcDevice;
        [SerializeField] private LocalizedString gamepadDevice;
        [SerializeField] private LocalizedString eraseQuestion;

        [Header("Events")]
        [SerializeField] private UnityEvent onClose;

        private InputOptionsData inputData;

        protected override float TimeOut => GameSettings.UI.RebindTimeOut;
        private string[] Devices => new string[] { pcDevice, gamepadDevice };

        protected override void Awake()
        {
            base.Awake();
            deviceNavigator.Init(Devices, 0);
            LocalizationManager.OnLocalizeEvent += OnUpdateNavigator;
        }

        private void OnUpdateNavigator()
        {
            deviceNavigator.UpdateTexts(Devices);
        }

        public void LoadInputSettings()
        {
            bool hasData = PersistenceManager.ContainsGlobalFile<InputOptionsData>();
            if (hasData)
            {
                inputData = PersistenceManager.LoadGlobalFile<InputOptionsData>();
            }
            else
            {
                inputData = new InputOptionsData();
                PersistenceManager.SaveGlobalFile(inputData);
            }

            LoadBindingOverrides(inputData.bindingOverrides);
        }

        protected override void SaveBindingOverrides(string json)
        {
            inputData.bindingOverrides = json;
            PersistenceManager.SaveGlobalFile(inputData);
        }

        #region Button Events
        public void OnChangePanel(int index)
        {
            bool gamepad = index == 1;

            Navigation navigation = deviceNavigator.navigation;
            navigation.selectOnDown = gamepad ? gamepadFirstBtn : keyboardFirstBtn;
            deviceNavigator.navigation = navigation;

            gamepadPanel.SetActive(gamepad);
            keyboardPanel.SetActive(!gamepad);
        }

        public void OnCloseScreen() => onClose.Invoke();
        #endregion
    }
}