using Z3.UIBuilder.Core;
using UnityEngine;
using UnityEngine.Events;

namespace AdventureGame.UI.Window
{
    /// <summary>
    /// Generic and basic implementation
    /// </summary>
    public class SimpleWindow : MonoBehaviour, IWindow
    {
        [Title("Simple Window")]
        [SerializeField] private GameObject firstBtn;
        [Space]
        [SerializeField] private UnityEvent onOpen;
        [SerializeField] private UnityEvent onClose;

        public GameObject FirstGameObject => firstBtn;

        public virtual void OpenWindow()
        {
            gameObject.SetActive(true);
            onOpen.Invoke();
        }

        public virtual void CloseWindow()
        {
            gameObject.SetActive(false);
            onClose.Invoke();
        }

        public void OnRequestToClose()
        {
            this.TryCloseWindow();
        }

        public void OnRequestToOpen()
        {
            this.RequestOpenWindow();
        }
    }
}