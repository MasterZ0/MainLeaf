using AdventureGame.Shared.ExtensionMethods;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AdventureGame.UIElements
{
    /// <summary>
    /// Control which element should appear
    /// </summary>
    public class TabGroup : MonoBehaviour
    {
        [Title("Tab Group")]
        [SerializeField] private bool rememberLastTab;
        [SerializeField] private UnityEvent onCloseTab;
        [SerializeField] private List<TabPair> tabPair;

        private TabPair currentTab;

        private void Awake()
        {
            foreach (TabPair btn in tabPair)
            {
                btn.Init(this);
            }
        }

        private void OnEnable()
        {
            if (currentTab == null)
            {
                currentTab = tabPair[0];
                currentTab.Show();
                return;
            }

            TabPair firstTap = rememberLastTab ? currentTab : tabPair[0];
            ShowTab(firstTap);
        }

        public void NavigateRight() => Navigate(true);

        public void NavigateLeft() => Navigate(false);

        private void Navigate(bool goRight)
        {
            int index = tabPair.IndexOf(currentTab);
            index = index.Navigate(tabPair.Count, goRight);
            ShowTab(tabPair[index]);
        }

        internal void ShowTab(TabPair tab)
        {
            currentTab.Hide();
            currentTab = tab;
            currentTab.Show();
        }

        public void RequestCloseTab() => onCloseTab.Invoke();
    }
}