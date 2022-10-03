using AdventureGame.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AdventureGame.MainMenu
{
    public class CharacterPreviewPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameTMP;
        [SerializeField] private Slider style;
        [SerializeField] private GameObject[] damage;
        [SerializeField] private GameObject[] agility;
        [SerializeField] private GameObject[] support;
        [SerializeField] private GameObject[] resistence;

        private CharacterPreview currentCharacter;

        public void ShowInfo(CharacterPreview preview)
        {
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
        }

        public void OnCancel()
        {
            currentCharacter.Select();
        }
    }
}