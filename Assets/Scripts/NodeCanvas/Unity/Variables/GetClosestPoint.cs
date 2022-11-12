using System.Collections.Generic;
using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using Header = ParadoxNotion.Design.HeaderAttribute;

namespace AdventureGame.NodeCanvas.Unity.NodeCanvas.Unity
{
    [Category(Categories.Variables)]
    [Description("Gets the closest point from a transform inside a list")]
    public class GetClosestPoint<T> : ActionTask<Transform> where T : Component
    {
        [Header("In")]
        public BBParameter<List<T>> points;
        
        [Header("Out")]
        public BBParameter<int> outIndex;
        public BBParameter<T> outPoint;

        protected override void OnExecute()
        {
            float closestDistance = GetSqrDistance(0);
            int closestIndex = 0;
            
            for (int i = 1; i < points.value.Count; i++)
            {
                float currentDistance = GetSqrDistance(i);
                
                if (currentDistance < closestDistance)
                {
                    closestDistance = currentDistance;
                    closestIndex = i;
                }
            }

            outIndex.value = closestIndex;
            outPoint.value = points.value[closestIndex];
            EndAction(true);
        }

        private float GetSqrDistance(int index)
        {
            Vector3 point = points.value[index].transform.position;
            return (point - agent.position).sqrMagnitude;
        }
    }
}