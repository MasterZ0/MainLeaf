using NodeCanvas.Framework;
using ParadoxNotion;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity.Physic
{

    [Category("AdventureGame/Physic/Rigidbody2D")]
    [Description("Compare the Transform Y position")]
    [Name("Check Y Position")]
    public class CheckYPosition : ConditionTask<Transform>
    {

        public BBParameter<float> position;
        public CompareMethod checkType = CompareMethod.EqualTo;

        protected override string info => "Position" + OperationTools.GetCompareString(checkType) + position;

        protected override bool OnCheck()
        {
            return OperationTools.Compare(agent.transform.position.y, position.value, checkType, 0);
        }
    }
}