using AdventureGame.Shared.NodeCanvas;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity 
{
    [NodeCategory(Categories.Components)]
    [NodeDescription("MonoBehaviour.enable = active")]
    public class SetActiveComponent : ActionTask<Behaviour> 
    {
        /*[RequiredField]*/ public Parameter<bool> active;

        public override string Info => active.Value ? 
            $"Enable {AgentInfo}" : 
            $"Disable {AgentInfo}";

        protected override void StartAction() {
            Agent.enabled = active.Value;
            EndAction(true);
        }
    }
}