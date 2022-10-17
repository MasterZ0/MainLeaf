using NodeCanvas.Framework;
using AdventureGame.Shared.NodeCanvas;
using ParadoxNotion.Design;
using UnityEngine;
using ParadoxNotion;
using AdventureGame.Shared.ExtensionMethods;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Transform)]
    [Description("Check the distance beetwen the reference to the target comparing the selected axis.")]
    public class CheckAxisDistance : ConditionTask
    {
        public BBParameter<Vector3> reference;
        public BBParameter<Vector3> target;
        public BBParameter<float> distance;
        public BBParameter<AxisFlags> axis;
        public CompareMethod checkType = CompareMethod.LessOrEqualTo;

        protected override string info
        {
            get
            {
                return $"{axis} Distance {reference} to {target}" + OperationTools.GetCompareString(checkType) + distance;
            }
        }

        protected override bool OnCheck()
        {
            float axisDistance = axis.value.Distance(reference.value, target.value);
            return OperationTools.Compare(axisDistance, distance.value, checkType, 0f);
        }
    }
}