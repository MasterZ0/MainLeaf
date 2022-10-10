using AdventureGame.Shared.NodeCanvas;
using AdventureGame.Shared.Utils;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using HeaderAttribute = ParadoxNotion.Design.HeaderAttribute;

namespace AdventureGame.NodeCanvas.Unity 
{
    [Category(Categories.Variables)]
    [Description("Get angle based from center to target")]
    public class GetTargetAngle : ActionTask {

        [Header("In")]
        public BBParameter<Vector3> center;
        public BBParameter<Vector3> target;

        [Header("Out")]
        public BBParameter<float> angle;

        protected override string info => $"{angle} = Between {center} to {target}";

        protected override void OnExecute() {
            angle.value = MathUtils.TargetAngle(center.value, target.value);
            EndAction(true);
        }
    }
}