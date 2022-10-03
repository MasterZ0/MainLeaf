using AdventureGame.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AdventureGame.MainMenu
{
    public class CharacterPreview : Selectable
    {
        [Header("Character Preview")]
        [SerializeField] private Animator nameAnimator;
        [SerializeField] private CharacterSettings characterSettings;

        public CharacterSettings CharacterSettings => characterSettings;

        private UISettings Settings => GameSettings.UI;

        public void Init(CharacterPreviewPanel previewPanel)
        {
            previewPanel.ShowInfo(this);
        }

        public override void OnSelect(BaseEventData eventData)
        {
            base.Select();
            //SetColor(Settings.OutlineSelectedColor);
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
            //SetColor(Color.clear);
        }

        //private void SetColor(Color color)
        //{
        //    MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();

        //    for (int i = 0; i < renderers.Length; i++)
        //    {
        //        //propertyBlock[i] = new MaterialPropertyBlock();

        //        renderers[i].GetPropertyBlock(propertyBlock);
        //        propertyBlock.SetColor(SelectionColor, color);
        //        renderers[i].SetPropertyBlock(propertyBlock);
        //    }
        //}
    }
}