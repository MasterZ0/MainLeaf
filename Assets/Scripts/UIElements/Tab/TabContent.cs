using UnityEngine;
using UnityEngine.EventSystems;

namespace AdventureGame.UIElements
{
    public class TabContent : MonoBehaviour
    {
        [SerializeField] private GameObject firstGameObject;

        private TabPair tabPair;

        public void OnRequestClose() => tabPair.RequestCloseContent();

        internal void Init(TabPair controller) => tabPair = controller;

        internal void Show()
        {
            gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(firstGameObject);
        }

        internal void Hide() => gameObject.SetActive(false);
    }
}