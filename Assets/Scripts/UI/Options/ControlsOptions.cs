using AdventureGame.Data;
using AdventureGame.Inputs;
using AdventureGame.Persistence;
using AdventureGame.UIElements;
using I2.Loc;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace AdventureGame.UI.AppOptions
{
    public class ControlsOptions : InputRebinderManager
    {
        [Title("Controls Settings")]
        [SerializeField] private Navigator deviceNavigator;
        [SerializeField] private GameObject pcPanel;
        [SerializeField] private GameObject gamepadPanel;
        [SerializeField] private Button pcFirstBtn;
        [SerializeField] private Button gamepadFirstBtn;

        [Header("Texts")]
        [SerializeField] private LocalizedString pcDevice;
        [SerializeField] private LocalizedString gamepadDevice;
        [SerializeField] private LocalizedString eraseQuestion;

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
            inputData = PersistenceManager.LoadGlobalFile<InputOptionsData>();

            if (inputData == null)
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
            navigation.selectOnDown = gamepad ? gamepadFirstBtn : pcFirstBtn;
            deviceNavigator.navigation = navigation;

            gamepadPanel.SetActive(gamepad);
            pcPanel.SetActive(!gamepad);
        }

        public void OnEraseAll()
        {
            UIManager.Popup.RequestQuestion(eraseQuestion, OnAnswer, false);

            void OnAnswer(bool result)
            {
                if (result)
                {
                    ResetAllInputs();
                }

                deviceNavigator.Select();
            }
        }
        #endregion
    }
}