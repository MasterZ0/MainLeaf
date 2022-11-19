using AdventureGame.Data;
using I2.Loc;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AdventureGame.MainMenu
{
    public class SelectableCharacter : Selectable, ISubmitHandler, ICancelHandler, IPointerClickHandler
    {
        [Header("Selectable Character")]
        [SerializeField] private PlayerSettings characterSettings;

        [Header("Components")]
        [SerializeField] private GameObject characterCam;
        [SerializeField] private Localize worldName;
        [SerializeField] private Outline outline;

        [Header("Events")]
        [SerializeField] private UnityEvent onSubmit;

        public GameObject CharacterCam => characterCam;
        public PlayerSettings CharacterSettings => characterSettings;

        private UISettings Settings => GameSettings.UI;
        private CharacterSelection controller;

        public void Init(CharacterSelection characterSelection)
        {
            controller = characterSelection;
            worldName.SetTerm(characterSettings.CharacterName.mTerm);
        }

        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            outline.SetColor(Settings.OutlineSelectedColor);
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
            outline.SetColor(Color.clear);
        }

        public void OnSubmit(BaseEventData eventData) => OnSubmit();

        public void OnPointerClick(PointerEventData eventData) // TODO: This is not working
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                OnSubmit();
            }
        }

        public void OnCancel(BaseEventData eventData)
        {
            controller.Cancel();
        }

        private void OnSubmit()
        {
            controller.ShowInfo(this);
            onSubmit.Invoke();
        }
    }
}