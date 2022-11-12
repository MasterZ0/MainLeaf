using UnityEngine;
using UnityEngine.EventSystems;

namespace AdventureGame.UIElements
{
    public class AutoScrollItem : MonoBehaviour, ISelectHandler
    {
        private AutoScroll autoScroll;

        public virtual void Setup(AutoScroll autoScroll)
        {
            this.autoScroll = autoScroll;
        }

        public void OnSelect(BaseEventData eventData)
        {
            autoScroll.SelectItem(transform as RectTransform);
        }
    }
}