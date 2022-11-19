using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace AdventureGame.UIElements
{
    public class CustomUIElement : UIBehaviour, ICustomUIHandler, ICancelHandler, IPointerEnterHandler
    {
        [SerializeField] private UnityEvent onCancel;
        [SerializeField] private UnityEvent onExtraA;
        [SerializeField] private UnityEvent onExtraB;

        public void OnExtraA() => onExtraA.Invoke();

        public void OnExtraB() => onExtraB.Invoke();

        public void OnCancel(BaseEventData eventData) => onCancel.Invoke();

        public void OnPointerEnter(PointerEventData eventData) => EventSystem.current.SetSelectedGameObject(gameObject);
    }
}