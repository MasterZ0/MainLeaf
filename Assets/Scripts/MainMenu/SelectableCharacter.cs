using AdventureGame.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AdventureGame.MainMenu
{
    public class SelectableCharacter : Selectable
    {
        [Header("Selectable Character")]
        [SerializeField] private PlayerSettings characterSettings;

        [Header("Components")]
        [SerializeField] private GameObject characterCam;
        [SerializeField] private Outline outline;

        public GameObject CharacterCam => characterCam;
        public PlayerSettings CharacterSettings => characterSettings;

        private UISettings Settings => GameSettings.UI;
        private CharacterSelection controller;

        public void Init(CharacterSelection characterSelection)
        {
            controller = characterSelection;
        }

        public void OnSubmit()
        {
            controller.ShowInfo(this);
        }

        public void OnCancel()
        {
            controller.Cancel();
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
    }
}