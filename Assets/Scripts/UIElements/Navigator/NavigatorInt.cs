using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AdventureGame.UIElements
{
    public class NavigatorInt : Selectable, IEventSystemHandler, ISubmitHandler
    {
        [Header("Navigator")]
        [SerializeField] private Animator leftArrow;
        [SerializeField] private Animator rightArrow;
        [SerializeField] private TextMeshProUGUI valueTmp;

        [Space]
        [Tooltip("Send current index")]
        [SerializeField] public UnityEvent<int> onValueChange;
        [SerializeField] public UnityEvent<int> onSubmit;

        private Vector2Int range;
        private int currentValue;

        private int CurrentValue 
        {   
            get
            {
                return currentValue;
            }
            set
            {
                currentValue = value;
                valueTmp.text = currentValue.ToString();
            } 
        }

        private bool LeftEnd => currentValue == range.x;
        private bool RightEnd => currentValue == range.y;

        private const string End = "End";

        private const string Disabled = "Disabled";
        private const string Normal = "Normal";
        private const string Pressed = "Pressed";
        private const string Selected = "Selected";

        public void Init(Vector2Int valueRange, int initialValue)
        {
            range = valueRange;
            SetValue(initialValue);
        }

        public void SetValue(int initialValue)
        {
            if (initialValue < range.x || initialValue > range.y)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            CurrentValue = initialValue;
            UpdateEndArrow();
        }

        private void UpdateEndArrow()
        {
            leftArrow.SetBool(End, LeftEnd);
            rightArrow.SetBool(End, RightEnd);
        }

        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);

            leftArrow.Play(LeftEnd ? Disabled : Selected);
            rightArrow.Play(RightEnd ? Disabled : Selected);
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);

            leftArrow.Play(LeftEnd ? Disabled : Normal);
            rightArrow.Play(RightEnd ? Disabled : Normal);
        }

        public override Selectable FindSelectableOnLeft()
        {
            GoLeft();
            return null;
        }

        public override Selectable FindSelectableOnRight()
        {
            GoRight();
            return null;
        }
        public void GoLeft()
        {
            if (!Application.isPlaying || LeftEnd)
                return;

            CurrentValue--;

            leftArrow.Play(Pressed);
            rightArrow.Play(Selected);

            UpdateAndInvoke();
        }

        public void GoRight()
        {
            if (!Application.isPlaying || RightEnd)
                return;

            CurrentValue++;

            leftArrow.Play(Selected);
            rightArrow.Play(Pressed);

            UpdateAndInvoke();
        }

        private void UpdateAndInvoke()
        {
            UpdateEndArrow();

            onValueChange.Invoke(currentValue);
        }

        public void OnSubmit(BaseEventData eventData)
        {
            onSubmit.Invoke(currentValue);
        }
    }
}