using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour {
    [System.Serializable]
    private struct Character {
        [Header("Character")]
        public string name;
        [Range(0, 1)] public float style;
        [Range(1, 5)] public int damage;
        [Range(1, 5)] public int agility;
        [Range(1, 5)] public int support;
        [Range(1, 5)] public int resistence;

        [Header("- Config")]
        public Animator nameAnimator;
        public SelectionColor[] selection;
    }

    [Header("Character Selection")]
    [SerializeField] private Character[] characters;

    [Header(" - Event")]
    [SerializeField] private IntEvent onSubmit;
    [SerializeField] private UnityEvent onSelect;
    [SerializeField] private UnityEvent onCancel;

    [Header(" - Panel")]
    [SerializeField] private TextMeshProUGUI nameTMP;
    [SerializeField] private Slider style;
    [SerializeField] private GameObject[] damage;
    [SerializeField] private GameObject[] agility;
    [SerializeField] private GameObject[] support;
    [SerializeField] private GameObject[] resistence;

    private Controls controls;
    private int characterIndex;

    private void Awake() {
        controls = new Controls();
        controls.UI.Navigate.started += ctx => OnNavigate(ctx.ReadValue<Vector2>().x);
        controls.UI.Submit.started += ctx => OnSubmit();
        controls.UI.Cancel.started += ctx => OnCancel();
    }
    /// <summary>
    /// Enable character selection controls
    /// </summary>
    public void SetActive() { 
        controls.Enable();
        onSelect.Invoke();  // Disable the first select
        characters[characterIndex].nameAnimator.SetBool(ConstAnimations.Navigation.SELECTED, true);

        foreach (SelectionColor sc in characters[characterIndex].selection) {
            sc.SetColor(Color.green);
        }
    }

    private void OnSubmit() {
        controls.Disable();
        onSubmit.Invoke(characterIndex);
        characters[characterIndex].nameAnimator.SetBool(ConstAnimations.Navigation.SELECTED, false);
        UpdatePanel();

        foreach (SelectionColor sc in characters[characterIndex].selection) {
            sc.SetColor(Color.black);
        }
    }
    private void OnNavigate(float direction) {
        if (direction == 0)
            return;

        characters[characterIndex].nameAnimator.SetBool(ConstAnimations.Navigation.SELECTED, false);
        characterIndex = characterIndex.Navigate(characters.Length, direction > 0);

        characters[characterIndex].nameAnimator.SetBool(ConstAnimations.Navigation.SELECTED, true);
        onSelect.Invoke();

        for (int i = 0; i < characters.Length; i++) {
            Color color = i == characterIndex ? Color.green : Color.black;

            foreach (SelectionColor sc in characters[i].selection) {
                sc.SetColor(color);
            }
        }
    }
    private void OnCancel() {
        controls.Disable();
        onCancel.Invoke();

        characters[characterIndex].nameAnimator.SetBool(ConstAnimations.Navigation.SELECTED, false);
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
