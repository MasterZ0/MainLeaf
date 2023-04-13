using AdventureGame.Shared.NodeCanvas;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using UnityEngine;
using Z3.NodeGraph.Tasks;

namespace AdventureGame.NodeCanvas.Unity
{
    [NodeCategory(Categories.Animations)]
    [NodeDescription("Check the current animation by state name")]
    public class CheckAnimation : ConditionTask<Animator>
    {
        public Parameter<string> stateName;

        public override string Info => $"Animation == {stateName}";

        public override bool CheckCondition()
        {
            var stateInfo = Agent.GetCurrentAnimatorStateInfo(0);
            return stateInfo.IsName(stateName.Value);
        }
    }
}