using AdventureGame.Shared.ExtensionMethods;
using AdventureGame.Shared.NodeCanvas;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace AdventureGame.NodeCanvas.Analyzers {

    [NodeCategory(Categories.Analyzers)]
    [NodeDescription("Filters input indexes considering whether the target is within or outside the average distance")]
    public class AverageDistance : ActionTask<Transform> {

        [Header("In")]
        /*[RequiredField]*/ public Parameter<List<float>> enter;
        /*[RequiredField]*/ public Parameter<Vector3> target;

        [Header("Config")]
        public Axis3Flags axis;
        public Parameter<Vector2> averageDistance;
        public int[] shortRemovedIndex;
        public int[] avarageRemovedIndex;
        public int[] longRemovedIndex;

        [Header("Out")]
        public Parameter<List<float>> resultObject;

        protected override void StartAction()
        {
            float distance = axis.Distance(Agent.position, target.Value);
            if (distance < averageDistance.Value.x)
            {
                resultObject.Value = RemoveIndexs(shortRemovedIndex);
            }
            else if (distance > averageDistance.Value.y)
            {
                resultObject.Value = RemoveIndexs(longRemovedIndex);
            }
            else
            {
                resultObject.Value = RemoveIndexs(avarageRemovedIndex);
            }

            EndAction(true);
        }

        private List<float> RemoveIndexs(int[] removedIndex) {

            List<float> result = new List<float>(enter.Value);
            foreach (int index in removedIndex) {
                result[index] = 0;
            }
            return result;
        }
    }
}