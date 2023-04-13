using AdventureGame.Shared.NodeCanvas;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;

namespace AdventureGame.NodeCanvas.Unity
{
    [NodeCategory(Categories.Variables)]
    [NodeDescription("Best way to check if some object is null")]
    public class CheckNull : ConditionTask 
    {
        //[BlackboardOnly]
        public Parameter<object> variable;

        public override string Info {
            get { return variable + " == null"; }
        }

        public override bool CheckCondition() {
            return variable.isNull;
        }
    }
}