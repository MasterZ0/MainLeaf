using NodeCanvas.Framework;
using AdventureGame.Shared.NodeCanvas;
using ParadoxNotion.Design;
using System.Collections.Generic;
using UnityEngine;
using HeaderAttribute = ParadoxNotion.Design.HeaderAttribute;

namespace AdventureGame.NodeCanvas.Analyzers
{
    [Category(Categories.Analyzers)]
    [Description("Check if the agent is inside the limits")]
    public class WithinAnalizer : ActionTask<Transform>
    {
        [Header("In")]
        [RequiredField] public BBParameter<List<float>> chanceEnter;

        [Header("Config")]
        public Axis axis;
        public BBParameter<Vector3> leftUpLimit;
        public BBParameter<Vector3> rightDownLimit;
        public int[] insideRemovedIndex;
        public int[] outRemovedIndex;

        [Header("Out")]
        public BBParameter<List<float>> resultChance;

        protected override void OnExecute()
        {
            if (IsInside())
            {
                resultChance.value = RemoveIndexs(insideRemovedIndex);
            }
            else
            {
                resultChance.value = RemoveIndexs(outRemovedIndex);
            }

            EndAction(true);
        }

        private bool IsInside()
        {
            switch (axis)
            {
                case Axis.X:
                    return InsideX();
                case Axis.Y:
                    return InsideY();
                case Axis.Both:
                    return InsideX() && InsideY();
                default:
                    throw new System.NotImplementedException();
            }
        }

        private bool InsideX()
        {
            if (agent.position.x > leftUpLimit.value.x && agent.position.x < rightDownLimit.value.x)
            {
                return true;
            }

            return false;
        }

        private bool InsideY()
        {
            if (agent.position.y < leftUpLimit.value.y && agent.position.y > rightDownLimit.value.y)
            {
                return true;
            }

            return false;
        }


        private List<float> RemoveIndexs(int[] removedIndex)
        {

            List<float> result = new List<float>(chanceEnter.value);
            foreach (int index in removedIndex)
            {
                result[index] = 0;
            }
            return result;
        }

    }
    
}