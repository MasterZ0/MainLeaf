using AdventureGame.Inputs;
using UnityEngine;
using UnityEngine.Events;

namespace AdventureGame.UI
{
    public class TabNavigator : MonoBehaviour
    {
        [SerializeField] private UnityEvent onLeftTab;
        [SerializeField] private UnityEvent onRightTab;

        private UIInputs uiInputs;

        private void Awake()
        {
            uiInputs = new UIInputs(false);
            uiInputs.OnLeftTab += onLeftTab.Invoke;
            uiInputs.OnRightTab += onRightTab.Invoke;
        }

        private void OnEnable() => uiInputs.SetActive(true);

        private void OnDisable() => uiInputs.SetActive(false);

        private void OnDestroy() => uiInputs.Dispose();
    }
}