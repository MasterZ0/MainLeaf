using Z3.UIBuilder.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AdventureGame.UIElements
{
    /// <summary>
    /// Displays content when clicked
    /// </summary>
    public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
    {
        [Title("Tab Button")]
        [SerializeField] private Animator animator;
        [SerializeField] private AnimationTriggers animationTriggers;

        private TabPair tabPair;
        private bool selected;

        internal void Init(TabPair controller) => tabPair = controller;

        public void Select()
        {
            selected = true;
            animator.SetTrigger(animationTriggers.selectedTrigger);
        }

        public void Deselect()
        {
            selected = false;
            animator.SetTrigger(animationTriggers.normalTrigger);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (selected)
                return;

            if (eventData.button == PointerEventData.InputButton.Left)
            {
                tabPair.RequestOpenTab();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (selected)
                return;

            animator.SetTrigger(animationTriggers.highlightedTrigger);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (selected)
                return;

            animator.SetTrigger(animationTriggers.normalTrigger);
        }
    }
}