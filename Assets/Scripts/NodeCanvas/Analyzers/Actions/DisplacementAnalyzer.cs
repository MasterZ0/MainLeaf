using NodeCanvas.Framework;
using AdventureGame.Shared.NodeCanvas;
using ParadoxNotion.Design;
using System.Collections.Generic;
using UnityEngine;
using Header = ParadoxNotion.Design.HeaderAttribute;

namespace AdventureGame.NodeCanvas.Analyzers {

    [Category(Categories.Analyzers)]
    [Description("Filters the input indexes considering whether the desired movement is within the defined limits")]
    public class DisplacementAnalyzer : ActionTask<Transform> {

        [Header("In")]
        [RequiredField] public BBParameter<List<float>> chanceEnter;
        public BBParameter<Vector3> displacementSpace;
        public BBParameter<Vector3> leftUpLimit;
        public BBParameter<Vector3> rightDownLimit;

        [Header("Config")]
        public Axis axis;
        public int[] outRemovedIndex;
        public int[] insideRemovedIndex;

        [Header("Out")]
        public BBParameter<List<float>> resultChance;

        protected override void OnExecute() {
            if (IsInside()) {
                resultChance.value = RemoveIndexs(insideRemovedIndex);
                EndAction(true);
            }
            else {
                resultChance.value = RemoveIndexs(outRemovedIndex);
                EndAction(true);
            }
        }

        private bool IsInside() {
            Vector3 finalPosition;

            if (agent.right.x > 0) { // Is Looking Right
                finalPosition = agent.position + displacementSpace.value;
            }
            else {
                finalPosition = agent.position - displacementSpace.value;
            }

            switch (axis) { // Could be switch expression
                case Axis.X:
                    return InsideX(finalPosition.x);
                case Axis.Y:
                    return InsideY(finalPosition.y);
                case Axis.Both:
                    return InsideX(finalPosition.x) && InsideY(finalPosition.y);
                default:
                    throw new System.NotImplementedException();
            }
        }

        private bool InsideY(float point) {
            float Up = leftUpLimit.value.y;
            float Down = rightDownLimit.value.y;

            if (point > Down && point < Up) {
                return true;
            }
            return false;
        }


        private bool InsideX(float point) {
            float Left = leftUpLimit.value.x;
            float Right = rightDownLimit.value.x;

            if (point > Left && point < Right) {
                return true;
            }
            return false;
        }

        private List<float> RemoveIndexs(int[] removedIndex) {

            List<float> result = new List<float>(chanceEnter.value);
            foreach (int index in removedIndex) {
                result[index] = 0;
            }
            return result;
        }
    }
}