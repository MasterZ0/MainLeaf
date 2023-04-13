using AdventureGame.Shared.NodeCanvas;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{

    [NodeCategory(Categories.Components)]
    [NodeDescription("Stop a particle system.")]
    public class StopParticleSystem : ActionTask<ParticleSystem>
    {
        public override string Info => $"Stop {AgentInfo}";

        public Parameter<bool> stopChildren;

        protected override void StartAction()
        {
            Agent.Stop(stopChildren.Value);
            EndAction(true);
        }
    }
}