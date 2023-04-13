using AdventureGame.Shared.NodeCanvas;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [NodeCategory(Categories.Transform)]
    [NodeDescription("GameObject.SetActive(active)")]
    public class SetActiveGameObject : ActionTask<Transform> 
    {
        /*[RequiredField]*/ public Parameter<bool> active;

        public override string Info => active.Value ?
            $"Enable {AgentInfo}" : $"Disable {AgentInfo}";

        protected override void StartAction() {
            Agent.gameObject.SetActive(active.Value);
            EndAction(true);
        }
    }
}