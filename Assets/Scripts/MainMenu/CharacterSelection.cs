using AdventureGame.Inputs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AdventureGame.MainMenu
{
    public class CharacterSelection : MonoBehaviour
    {
        [Header("Character Selection")]
        [SerializeField] private SelectableCharacter[] characters;
        [SerializeField] private GameObject[] charactersCam;
        [SerializeField] private MainMenu mainMenu;

        [Header(" - Event")]
        [SerializeField] private UnityEvent<int> onSubmit;
        [SerializeField] private UnityEvent onSelect;
        [SerializeField] private UnityEvent onCancel;

        private int characterIndex;

        /// <summary>
        /// Enable character selection controls
        /// </summary>
        public void SetActive()
        {
            onSelect.Invoke(); 
            characters[characterIndex].Select();

            mainMenu.SwitchCamera(charactersCam[characterIndex]);
        }

        private void OnSubmit()
        {
            onSubmit.Invoke(characterIndex);
        }
        private void OnCancel()
        {
            onCancel.Invoke();
        }
    }
}