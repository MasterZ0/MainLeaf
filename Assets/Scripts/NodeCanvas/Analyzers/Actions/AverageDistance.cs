using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
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
        public Axis axis;
        public BBParameter<Vector2> averageDistance;
        public int[] shortRemovedIndex;
        public int[] avarageRemovedIndex;
        public int[] longRemovedIndex;

        [Header("Out")]
        public BBParameter<List<float>> resultObject;

        protected override void OnExecute() {
            float distance;

            switch (axis) {     // Could be switch expression
                case Axis.X:
                    distance = Mathf.Abs(agent.position.x - target.value.x);
                    break;
                case Axis.Y:
                    distance = Mathf.Abs(agent.position.y - target.value.y);
                    break;
                case Axis.Both:
                    distance = Vector2.Distance(agent.position, target.value);
                    break;
                default:
                    throw new System.NotImplementedException();
            }

            SetResult(distance);
            EndAction(true);
        }

        private void SetResult(float distance) {
            if (distance < averageDistance.value.x) {
                resultObject.value = RemoveIndexs(shortRemovedIndex);
            }
            else if (distance > averageDistance.value.y) {
                resultObject.value = RemoveIndexs(longRemovedIndex);
            }
            else {
                resultObject.value = RemoveIndexs(avarageRemovedIndex);
            }
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