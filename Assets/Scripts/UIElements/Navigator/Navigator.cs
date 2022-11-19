using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AdventureGame.UIElements
{
    public class Navigator : Selectable
    {
        [Header("Navigator")]
        [SerializeField] private Animator leftArrow;
        [SerializeField] private Animator rightArrow;
        [SerializeField] private TextMeshProUGUI value;

        [Space]
        [Tooltip("Send current index")]
        [SerializeField] public UnityEvent<int> onValueChange;

        private string[] displayPackage = System.Array.Empty<string>();
        private int index;

        private bool LeftEnd => index == 0;
        private bool RightEnd => index == displayPackage.Length - 1;

        private string Disabled => animationTriggers.disabledTrigger;
        private string Normal => animationTriggers.normalTrigger;
        private string Pressed => animationTriggers.pressedTrigger;
        private string Selected => animationTriggers.selectedTrigger;

        public void Init(string[] stringPackege, int currentIndex)
        {
            displayPackage = stringPackege;
            SetIndex(currentIndex);
        }

        public void SetIndex(int currentIndex)
        {
            if (currentIndex < 0 || currentIndex >= displayPackage.Length)
                throw new System.ArgumentOutOfRangeException($"Index out of range. Length: {displayPackage.Length}, Current Index: {currentIndex}");

            index = currentIndex;
            value.text = displayPackage[index];
        }

        public void UpdateTexts(string[] stringPackege)
        {
            if (index >= stringPackege.Length)
                throw new System.ArgumentOutOfRangeException("Check the string count");

            displayPackage = stringPackege;
            value.text = displayPackage[index];
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            leftArrow.SetBool(Disabled, LeftEnd);
            rightArrow.SetBool(Disabled, RightEnd);
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

            index--;

            leftArrow.Play(Pressed);
            rightArrow.Play(Selected);

            UpdateAndInvoke();
        }

        public void GoRight()
        {
            if (!Application.isPlaying || RightEnd)
                return;

            index++;

            leftArrow.Play(Selected);
            rightArrow.Play(Pressed);

            UpdateAndInvoke();
        }

        private void UpdateAndInvoke()
        {
            value.text = displayPackage[index];

            leftArrow.SetBool(Disabled, LeftEnd);
            rightArrow.SetBool(Disabled, RightEnd);

            onValueChange.Invoke(index);
        }
    }
}