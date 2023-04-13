using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Z3.UIBuilder.Core;

namespace AdventureGame.UIElements
{
    public class AutoScroll : MonoBehaviour
    {
        [Title("Auto Scroll")]
        [SerializeField] private RectTransform viewport;
        [SerializeField] private RectTransform content;
        [SerializeField] private List<AutoScrollItem> items;

        public RectTransform Content => content;

        private float minViewport;
        private float maxViewport;

        private bool useDelay = true;

        private void Awake()
        {
            Vector3[] corners = new Vector3[4]; // 0: BottomLeft, 1: UpLeft, 2: UpRight, 3: BottomRight
            viewport.GetWorldCorners(corners);

            minViewport = corners[0].y;
            maxViewport = corners[1].y;

            foreach (AutoScrollItem item in items)
            {
                item.Setup(this);
            }
        }

        private void OnDisable() => useDelay = true;

        public void SelectItem(RectTransform itemTransform)
        {
            if (useDelay)
            {
                StartCoroutine(SelectItemWithDelay(itemTransform));
            }
            else
            {
                UpdateView(itemTransform);
            }
        }

        private IEnumerator SelectItemWithDelay(RectTransform itemTransform)
        {
            yield return new WaitForEndOfFrame();
            useDelay = false;
            UpdateView(itemTransform);
        }

        private void UpdateView(RectTransform itemTransform)
        {
            Vector3[] corners = new Vector3[4];
            itemTransform.GetWorldCorners(corners);

            float minItem = corners[0].y;
            float maxItem = corners[1].y;

            if (maxItem > maxViewport)
            {
                float difference = maxItem - maxViewport;
                Vector2 currentPos = content.position;
                content.position = new Vector2(currentPos.x, currentPos.y - difference);
            }
            else if (minItem < minViewport)
            {
                float difference = minItem - minViewport;
                Vector2 currentPos = content.position;
                content.position = new Vector2(currentPos.x, currentPos.y - difference);
            }
        }
    }
}