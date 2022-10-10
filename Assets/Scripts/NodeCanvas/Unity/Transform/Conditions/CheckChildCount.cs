using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity { 

    [Category(Categories.Transform)]
    [Description("Compares how many children a transform has")]
    public class CheckChildCount : ConditionTask<Transform> {

        public CompareMethod checkType = CompareMethod.EqualTo;
        public BBParameter<int> value;

        protected override string info
        {
            get { return "Child Count" + OperationTools.GetCompareString(checkType) + value; }
        }

        protected override bool OnCheck()
        {
            return OperationTools.Compare(agent.childCount, value.value, checkType);
        }
    }
}