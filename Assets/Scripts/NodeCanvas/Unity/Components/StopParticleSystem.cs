using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{

    [Category(Categories.Components)]
    [Description("Stop a particle system.")]
    public class StopParticleSystem : ActionTask<ParticleSystem>
    {
        protected override string info => $"Stop {agentInfo}";

        public BBParameter<bool> stopChildren;

        protected override void OnExecute()
        {
            agent.Stop(stopChildren.value);
            EndAction(true);
        }
    }
}