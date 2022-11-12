using UnityEngine;
using UnityEngine.Events;

namespace AdventureGame.UI
{
    public class CustomEventTrigger : MonoBehaviour, IExtraButtonHandler
    {
        [SerializeField] private UnityEvent onExtraBtn;

        public void OnExtraButton() => onExtraBtn.Invoke();
    }
}