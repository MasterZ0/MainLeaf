using AdventureGame.Shared.NodeCanvas;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity { 

    [NodeCategory(Categories.Transform)]
    [NodeDescription("Compares how many children a transform has")]
    public class CheckChildCount : ConditionTask<Transform> {

        public CompareMethod checkType = CompareMethod.EqualTo;
        public Parameter<int> value;

        public override string Info
        {
            get { return "Child Count" + OperationTools.GetCompareString(checkType) + value; }
        }

        public override bool CheckCondition()
        {
            return OperationTools.Compare(Agent.childCount, value.Value, checkType);
        }
    }
}