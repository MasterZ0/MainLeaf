using AdventureGame.Shared.NodeCanvas;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [NodeCategory(Categories.Variables)]
    [NodeDescription("Useful to check if the object went to the pool or was destroyed")]
    public class CheckNullOrDisable : ConditionTask
    {
        //[BlackboardOnly]
        public Parameter<GameObject> variable;
        
        public override string Info
        {
            get { return variable + "is Null or Disabled"; }
        }

        public override bool CheckCondition()
        {
            return variable.isNull || !variable.Value.activeSelf;
        }
    }
}