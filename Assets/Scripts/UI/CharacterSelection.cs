using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour {

    [Header("Character Selection")]
    [SerializeField] private IntEvent onConfirm;
    [SerializeField] private UnityEvent onCancel;
    [SerializeField] private Animator[] characterSelection;
    [SerializeField] private Character[] characters;

    [Header(" - Panel")]
    [SerializeField] private TextMeshProUGUI nameTMP;
    [SerializeField] private Slider style;
    [SerializeField] private GameObject[] damage;
    [SerializeField] private GameObject[] agility;
    [SerializeField] private GameObject[] support;
    [SerializeField] private GameObject[] resistence;

    [System.Serializable]
    private struct Character {
        public string name;
        [Range(0, 1)] public float style;        
        [Range(1, 5)] public int damage;
        [Range(1, 5)] public int agility;
        [Range(1, 5)] public int support;
        [Range(1, 5)] public int resistence;

        [Space]
        public SelectionColor[] selection;
    }

    private Controls controls;
    private int characterIndex;

    private void Awake() {
        controls = new Controls();
        controls.UI.Navigate.started += ctx => OnChangeCharacter(ctx.ReadValue<Vector2>().x);
        controls.UI.Submit.started += ctx => OnConfirmCharacter();
        controls.UI.Cancel.started += ctx => OnCloseCharacterSeletion();
    }
    /// <summary>
    /// Enable character selection controls
    /// </summary>
    public void SetActive() { 
        controls.Enable();
        characterSelection[characterIndex].Play(Constants.Anim.SELECT);

        foreach (SelectionColor sc in characters[characterIndex].selection) {
            sc.SetColor(Color.green);
        }
    }
    private void OnChangeCharacter(float direction) {
        if (direction == 0)
            return;

        characterSelection[characterIndex].Play(Constants.Anim.DESELECT);
        characterIndex = characterIndex.Navigate(characters.Length, direction > 0);
        characterSelection[characterIndex].Play(Constants.Anim.SELECT);

        for (int i = 0; i < characters.Length; i++) {
            Color color = i == characterIndex ? Color.green : Color.black;

            foreach (SelectionColor sc in characters[i].selection) {
                sc.SetColor(color);
            }
        }
    }

    private void OnCloseCharacterSeletion() {
        controls.Disable();
        onCancel.Invoke();

        characterSelection[characterIndex].Play(Constants.Anim.DESELECT);
        foreach (SelectionColor sc in characters[characterIndex].selection) {
            sc.SetColor(Color.black);
        }
    }
    private void OnConfirmCharacter() {
        controls.Disable();
        onConfirm.Invoke(characterIndex);
        characterSelection[characterIndex].Play(Constants.Anim.DESELECT);
        UpdatePanel();

        foreach (SelectionColor sc in characters[characterIndex].selection) {
            sc.SetColor(Color.black);
        }
    }
    private void UpdatePanel() {
        Character current = characters[characterIndex];
        nameTMP.text = current.name;
        style.value = current.style;

        for (int i = 0; i < damage.Length; i++) {
            damage[i].SetActive(i <= current.damage - 1);
            agility[i].SetActive(i <= current.agility - 1);
            support[i].SetActive(i <= current.support - 1);
            resistence[i].SetActive(i <= current.resistence - 1);
        }
    }

}
