using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AdventureGame.UIElements
{
    /// <summary>
    /// Displays content when clicked
    /// </summary>
    public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
    {
        [Title("Tab Button")]
        [SerializeField] private Animator animator;

        private TabPair tabPair;

        private const string Selected = "Selected";
        private const string Normal = "Normal";

        internal void Init(TabPair controller) => tabPair = controller;

        public void Select() => animator.Play(Selected);

        public void Deselect() => animator.Play(Normal);

        public void OnPointerClick(PointerEventData eventData) => tabPair.RequestOpenTab();

        public void OnPointerEnter(PointerEventData eventData)
        {
            // highlight on
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            // highlight off
        }
    }
}