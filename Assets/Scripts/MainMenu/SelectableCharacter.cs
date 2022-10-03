using AdventureGame.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AdventureGame.MainMenu
{
    public class SelectableCharacter : Selectable
    {
        [Header("Selectable Character")]
        [SerializeField] private CharacterSettings characterSettings;

        [Header("Components")]
        [SerializeField] private Animator nameAnimator;
        [SerializeField] private Outline outline;

        public CharacterSettings CharacterSettings => characterSettings;

        private UISettings Settings => GameSettings.UI;

        public void Init(CharacterPreviewPanel previewPanel)
        {
            previewPanel.ShowInfo(this);
        }

        public override void OnSelect(BaseEventData eventData)
        {
            base.Select();
            outline.SetColor(Settings.OutlineSelectedColor);
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
            outline.SetColor(Color.clear);
        }
    }
}