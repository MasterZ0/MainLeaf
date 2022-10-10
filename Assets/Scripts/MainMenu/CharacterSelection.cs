using AdventureGame.Data;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AdventureGame.MainMenu
{
    public class CharacterSelection : MonoBehaviour
    {
        [Header("Character Selection")]
        [SerializeField] private MainMenu mainMenu;
        [SerializeField] private SelectableCharacter[] characters;

        [Header("Character Preview Panel")]
        [SerializeField] private TextMeshProUGUI nameTMP;
        [SerializeField] private Slider style;
        [SerializeField] private GameObject[] damage;
        [SerializeField] private GameObject[] agility;
        [SerializeField] private GameObject[] support;
        [SerializeField] private GameObject[] resistence;

        private SelectableCharacter currentCharacter;

        private void Awake()
        {
            currentCharacter = characters[0];

            foreach (SelectableCharacter character in characters)
            {
                character.Init(this);
            }
        }

        public void ShowInfo(SelectableCharacter preview)
        {
            mainMenu.ShowCharacter(preview.CharacterCam);

            currentCharacter = preview;

            nameTMP.text = preview.CharacterSettings.CharacterName;

            PlayerStatusSettings status = preview.CharacterSettings.Status;
            style.value = status.style;

            for (int i = 0; i < damage.Length; i++)
            {
                damage[i].SetActive(i <= status.damage - 1);
                agility[i].SetActive(i <= status.agility - 1);
                support[i].SetActive(i <= status.support - 1);
                resistence[i].SetActive(i <= status.resistence - 1);
            }

            EventSystem.current.SetSelectedGameObject(null);
        }

        internal void Cancel()
        {
            mainMenu.OnCloseCharacterSelection();
        }

        /// <summary>
        /// Enable character selection controls
        /// </summary>
        public void SelectCharacter()
        {
            currentCharacter.Select();
        }
    }
}