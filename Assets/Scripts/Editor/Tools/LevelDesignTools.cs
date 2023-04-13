using UnityEngine;
using UnityEditor;
using System;
using Object = UnityEngine.Object;
using Z3.UIBuilder.Core;

namespace AdventureGame.Editor
{
    [/*InlineProperty,*/ HideLabel, HideReferenceObjectPicker]
    public class LevelDesignTools
    {
        #region Rounder
        [Title("Rounder")]
        [Range(1, 4)/*, PropertyOrder(1)*/]
        public int divisionFactor = 2;

        //[ShowInInspector, PropertyOrder(0)]
        public int SelectedGameObjectCount => Selection.gameObjects.Length;

        [Button/*, PropertyOrder(2)*/]
        private void RoundTransform()
        {
            foreach (GameObject obj in Selection.gameObjects)
            {
                Undo.RecordObject(obj.transform, "roundedTransform");

                obj.transform.localPosition = Round(obj.transform.localPosition);
            }
        }

        [Button/*, PropertyOrder(3)*/]
        private void RoundColliders()
        {
            foreach (GameObject obj in Selection.gameObjects)
            {
                if (obj.TryGetComponent(out Collider2D current))
                {
                    Undo.RecordObject(current, "Rounded Collider");

                    current.offset = Round(current.offset);

                    if (current is PolygonCollider2D pollygon)
                    {
                        RoundPolygon(pollygon);
                    }
                    else if (current is BoxCollider2D box)
                    {
                        RoundBox(box);
                    }
                    else if (current is CircleCollider2D circle)
                    {
                        RoundCircle(circle);
                    }
                    else if (current is CapsuleCollider2D capsule)
                    {
                        RoundCapsule(capsule);
                    }
                    else if (current is EdgeCollider2D edge)
                    {
                        RoundEdge(edge);
                    }
                }
            }
        }
        private void RoundPolygon(PolygonCollider2D pollygon)
        {
            for (int i = 0; i < pollygon.pathCount; i++)
            {
                Vector2[] points = pollygon.GetPath(i);
                for (int j = 0; j < points.Length; j++)
                {
                    points[j] = Round(points[j]);
                }
                pollygon.SetPath(i, points);
            }
        }

        private void RoundBox(BoxCollider2D box)
        {
            box.size = Round(box.size);
            box.offset = Round(box.offset);
        }

        private void RoundCircle(CircleCollider2D circle)
        {
            circle.radius = Round(circle.radius);
        }

        private void RoundCapsule(CapsuleCollider2D capsule)
        {
            capsule.size = Round(capsule.size);
        }

        private void RoundEdge(EdgeCollider2D edge)
        {
            edge.edgeRadius = Round(edge.edgeRadius);
        }
        private Vector2 Round(Vector2 inicialValue)
        {
            Vector2 result;
            result.x = Round(inicialValue.x);
            result.y = Round(inicialValue.y);
            return result;
        }

        private float Round(float inicialValue)
        {
            float divisions = (int)Mathf.Pow(divisionFactor, 2f);
            return (float)Math.Round(inicialValue * divisions, MidpointRounding.AwayFromZero) / divisions;
        }
        #endregion

        #region Prefab Replacer
        [Title("Prefab Replacer")/*, PropertyOrder(4)*/]
        public Transform selectedPrefab;

        [Button/*, PropertyOrder(5)*/]
        private void Replace()
        {
            if (selectedPrefab != null)
            {
                foreach (Transform sceneObject in Selection.transforms)
                {
                    if (sceneObject == null)
                        continue;

                    Transform prefabInstance = PrefabUtility.InstantiatePrefab(selectedPrefab, sceneObject.gameObject.scene) as Transform;
                    prefabInstance.name = sceneObject.name;
                    prefabInstance.position = sceneObject.position;
                    prefabInstance.rotation = sceneObject.rotation;
                    prefabInstance.parent = sceneObject.parent;
                    Object.DestroyImmediate(sceneObject.gameObject);
                }
            }
        }
        #endregion
    }
}