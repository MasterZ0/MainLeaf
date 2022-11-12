using UnityEngine;

namespace AdventureGame.UIElements
{
    [System.Serializable]
    public class TabPair
    {
        [SerializeField] private TabButton buttons;
        [SerializeField] private TabContent content;

        private TabGroup tabGroup;

        internal void Init(TabGroup controller)
        {
            tabGroup = controller;
            buttons.Init(this);
            content.Init(this);
        }

        internal void Show()
        {
            buttons.Select();
            content.Show();
        }

        internal void Hide()
        {
            buttons.Deselect();
            content.gameObject.SetActive(false);
        }

        internal void RequestOpenTab()
        {
            tabGroup.ShowTab(this);
        }

        internal void RequestCloseContent()
        {
            tabGroup.RequestCloseTab();
        }
    }
}