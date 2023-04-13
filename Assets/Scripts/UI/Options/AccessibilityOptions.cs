using AdventureGame.Inputs;
using AdventureGame.Persistence;
using AdventureGame.Shake;
using AdventureGame.UIElements;
using I2.Loc;
using Z3.UIBuilder.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AdventureGame.UI.AppOptions
{
    public class AccessibilityOptions : MonoBehaviour
    {
        [Title("Accessibility")]
        [SerializeField] private LocalizedString on;
        [SerializeField] private LocalizedString off;

        [Header("Components")]
        [SerializeField] private Navigator shakeScreenNavigator;
        [SerializeField] private Navigator gamepadVibrationNavigator;
        [SerializeField] private Slider defaultSensitivity;
        [SerializeField] private TMP_Text defaultSensitivityTmp;
        [SerializeField] private Slider aimSensitivity;
        [SerializeField] private TMP_Text aimSensitivityTmp;

        public static float AimSensitivity => accessibilityData.aimSensitivity;
        public static float DefaultSensitivity => accessibilityData.defaultSensitivity;

        private static AccessibilityOptionsData accessibilityData;

        private string[] ToggleOptions => new string[] { off, on };

        private void Awake()
        {
            LocalizationManager.OnLocalizeEvent += UpdateFullScreenTexts;
        }

        private void OnDestroy()
        {
            LocalizationManager.OnLocalizeEvent -= UpdateFullScreenTexts;
        }

        private void OnDisable()
        {
            PersistenceManager.SaveGlobalFile(accessibilityData);
        }

        public void LoadSettings()
        {
            accessibilityData = PersistenceManager.LoadGlobalFile<AccessibilityOptionsData>();

            if (accessibilityData == null)
            {
                accessibilityData = new AccessibilityOptionsData();
                PersistenceManager.SaveGlobalFile(accessibilityData);
            }

            // Navigators
            shakeScreenNavigator.Init(ToggleOptions, accessibilityData.shakeScreen ? 1 : 0);
            Shaker.SetActiveShake(accessibilityData.shakeScreen);

            gamepadVibrationNavigator.Init(ToggleOptions, accessibilityData.gamepadVibration ? 1 : 0);
            InputManager.SetVibration(accessibilityData.shakeScreen);

            // Sliders
            defaultSensitivity.SetValueWithoutNotify(accessibilityData.defaultSensitivity);
            defaultSensitivityTmp.text = $"{accessibilityData.defaultSensitivity}";

            aimSensitivity.SetValueWithoutNotify(accessibilityData.aimSensitivity);
            aimSensitivityTmp.text = $"{accessibilityData.aimSensitivity}";
        }

        private void UpdateFullScreenTexts()
        {
            shakeScreenNavigator.UpdateTexts(ToggleOptions);
            gamepadVibrationNavigator.UpdateTexts(ToggleOptions);
        }

        public void OnSetShakeScreen(int value)
        {
            accessibilityData.shakeScreen = value == 1 ? true : false;
            Shaker.SetActiveShake(accessibilityData.shakeScreen);
        }

        public void OnSetGamepadVibration(int value)
        {
            accessibilityData.shakeScreen = value == 1 ? true : false;
            InputManager.SetVibration(accessibilityData.shakeScreen);
        }

        public void OnSetDefaultSensitivity(float value)
        {
            accessibilityData.defaultSensitivity = value;
            defaultSensitivityTmp.text = $"{value}";
        }

        public void OnSetAimSensitivity(float value)
        {
            accessibilityData.aimSensitivity = value;
            aimSensitivityTmp.text = $"{value}";
        }
    }
}