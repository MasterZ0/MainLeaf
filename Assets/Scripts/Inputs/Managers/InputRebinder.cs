using Sirenix.OdinInspector;
using System;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace AdventureGame.Inputs
{
    /// <summary>
    /// 
    /// </summary>
    public class InputRebinder : MonoBehaviour
    {
        [Title("Input Rebinder")]
        [OnValueChanged(nameof(ValidateInputActionReference))]
        [SerializeField] private InputActionReference inputReference;

        [OnValueChanged(nameof(ValidateBindIndex))]
        [EnableIf(nameof(BindIndexEnabled)), PropertyRange(0, nameof(maxIndex))]
        [SerializeField] private int bindIndex;

        [Title("Sprites")]
        [SerializeField] private Image inputIcon;
        [SerializeField] private GameObject pressToSelect;

        #region Dev Tools Variables
        [Title("Dev Tools")]
        [ShowInInspector, ReadOnly, InlineProperty, HideLabel, Title("Input Binding", HorizontalLine = false, Bold = false)]
        private InputBinding inputBinding;

        [Space, ShowInInspector, ReadOnly, TextArea]
        private string debugArea;
        private int maxIndex;
        private bool BindIndexEnabled => inputReference;
        #endregion

        public InputActionReference InputReference => inputReference;
        public int BindingIndex => bindIndex;

        private InputRebinderManager manager;

        #region Dev Tools Methods
        [OnInspectorInit]
        private void ValidateInputActionReference()
        {
            if (InputReference)
            {
                maxIndex = inputReference.action.bindings.Count - 1;
                if (bindIndex > maxIndex)
                    bindIndex = 0;
                ValidateBindIndex();
            }
            else
            {
                bindIndex = 0;
            }
        }

        private void ValidateBindIndex()
        {
            StringBuilder stringBuilder = new StringBuilder();

            inputBinding = inputReference.action.bindings[bindIndex]; 
            string text = InputControlPath.ToHumanReadableString(inputBinding.effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
            stringBuilder.AppendLine(text);
            string text2 = inputBinding.ToDisplayString();
            stringBuilder.AppendLine(text2);
            debugArea = stringBuilder.ToString();
        }
        #endregion

        public void Init(InputRebinderManager rebindManager)
        {
            manager = rebindManager;
        }

        public void RefreshIcon(Sprite icon)
        {
            inputIcon.sprite = icon;
        }

        public void OnRebind()
        {
            pressToSelect.SetActive(true);
            inputIcon.gameObject.SetActive(false);
            manager.DoRebind(this);
        }

        public void Select()
        {
            pressToSelect.SetActive(false);
            inputIcon.gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(gameObject);
        }

        public void OnCleanInput()
        {
            manager.CleanInput(this);
        }
    }
}