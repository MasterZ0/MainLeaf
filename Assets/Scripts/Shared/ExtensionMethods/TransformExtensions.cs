using System;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;

namespace AdventureGame.Shared.ExtensionMethods
{
    public static class TransformExtensions
    {
        public static Transform SearchChildren(this Transform parent, string desiredName)
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                Transform child = parent.GetChild(i);
                string treatedName = child.name.ToLower();
                
                if (treatedName.Contains(desiredName.ToLower()))
                    return child;

                Transform childrenResult = SearchChildren(child, desiredName);
                
                if (childrenResult != null)
                    return childrenResult;
            }

            return null;
        }
        
        public static Vector2 GetRealSize(this RectTransform rectTransform)
        {
            float width = Mathf.Abs(rectTransform.GetRealWidth());
            float height = Mathf.Abs(rectTransform.GetRealHeight());
            return new Vector2(width, height);
        }

        public static float GetRealWidth(this RectTransform rectTransform)
        {
            Vector3[] corners = rectTransform.GetWorldCorners();
            Vector3 leftBottom = corners[0];
            Vector3 rightTop = corners[2];
            return rightTop.x - leftBottom.x;
        }

        public static float GetRealHeight(this RectTransform rectTransform)
        {
            Vector3[] corners = rectTransform.GetWorldCorners();
            Vector3 leftBottom = corners[0];
            Vector3 rightTop = corners[2];
            return rightTop.y - leftBottom.y;
        }
        
        public static void SetWorldSize(this RectTransform rectTransform, Vector2 size)
        {
            Vector2 realSize = rectTransform.GetRealSize();
            Vector2 scaleMultiplier = size / realSize;
            Vector2 canvasSize = rectTransform.rect.size * scaleMultiplier;
            rectTransform.SetCanvasSize(canvasSize);
        }

        public static void SetCanvasSize(this RectTransform rectTransform, Vector2 size)
        {
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);
        }

        public static void ScaleAround(this Transform target, Vector3 pivot, Vector3 newScale)
        {
            Vector3 targetPosition = target.position;
            Vector3 difference = targetPosition - pivot;

            float relativeScale = newScale.x / target.localScale.x;
            Vector3 finalPosition = pivot + difference * relativeScale;

            target.localScale = newScale;
            target.position = finalPosition;
        }

        public static List<string> GetChildrenName(this Transform transform)
        {
            List<string> children = new List<string>();

            for (int i = 0; i < transform.childCount; i++)
            {
                Transform enemy = transform.GetChild(i);
                children.Add(enemy.name);
            }

            return children;
        }

        public static Vector3[] GetWorldCorners(this Transform unconvertedTransform)
        {
            Vector3[] corners = new Vector3[4];

            if (unconvertedTransform is RectTransform rect)
            {
                rect.GetWorldCorners(corners);
                return corners;
            }

            throw new NullReferenceException($"{unconvertedTransform} couldn't be converted to Rect Transform.");
        }
    }
}