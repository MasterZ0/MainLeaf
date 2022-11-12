using NodeCanvas.Framework;
using ParadoxNotion.Design;
using AdventureGame.Shared.NodeCanvas;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{

    [Category(Categories.Components)]
    [Description("Play a particle system.")]
    public class PlayParticleSystem : ActionTask<ParticleSystem>
    {
        protected override string info => $"Play {agentInfo}";

        public BBParameter<bool> playChildren;

        protected override void OnExecute()
        {
            agent.Play(playChildren.value);
            EndAction(true);
        }
    }
}