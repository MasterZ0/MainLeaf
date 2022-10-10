using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity {

    [Category(Categories.Rigidbody2D)]
    [Description("Compare the Rigidbody Y velocity")]
    [Name("Check Y Velocity")]
    public class CheckYVelocity : ConditionTask<Rigidbody2D> {

        public BBParameter<float> speed;
        public CompareMethod checkType = CompareMethod.EqualTo;

        protected override string info => "Velocity" + OperationTools.GetCompareString(checkType) + speed;

        protected override bool OnCheck() {
            return OperationTools.Compare(agent.velocity.y, speed.value, checkType, 0);
        }
    }
}