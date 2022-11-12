using AdventureGame.Shared.ExtensionMethods;
using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System;
using System.Collections.Generic;
using UnityEngine;
using Header = ParadoxNotion.Design.HeaderAttribute;

namespace AdventureGame.NodeCanvas.Analyzers {

    [Category(Categories.Analyzers)]
    [Description("Filters input indexes considering whether the target is within or outside the average distance")]
    public class AverageDistance : ActionTask<Transform> {

        [Header("In")]
        [RequiredField] public BBParameter<List<float>> enter;
        [RequiredField] public BBParameter<Vector3> target;

        [Header("Config")]
        public Axis3Flags axis;
        public BBParameter<Vector2> averageDistance;
        public int[] shortRemovedIndex;
        public int[] avarageRemovedIndex;
        public int[] longRemovedIndex;

        [Header("Out")]
        public BBParameter<List<float>> resultObject;

        protected override void OnExecute()
        {
            float distance = axis.Distance(agent.position, target.value);
            if (distance < averageDistance.value.x)
            {
                resultObject.value = RemoveIndexs(shortRemovedIndex);
            }
            else if (distance > averageDistance.value.y)
            {
                resultObject.value = RemoveIndexs(longRemovedIndex);
            }
            else
            {
                resultObject.value = RemoveIndexs(avarageRemovedIndex);
            }

            EndAction(true);
        }

        private List<float> RemoveIndexs(int[] removedIndex) {

            List<float> result = new List<float>(enter.value);
            foreach (int index in removedIndex) {
                result[index] = 0;
            }
            return result;
        }
    }
}