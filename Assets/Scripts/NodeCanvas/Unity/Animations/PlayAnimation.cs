using AdventureGame.Shared.NodeCanvas;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [NodeCategory(Categories.Animations)]
    [NodeDescription("Play animation by state name")]
    public class PlayAnimation : ActionTask<Animator>
    {
        public Parameter<bool> waitUntilFinish;
        public Parameter<string> stateName;
        public Parameter<int> layer;
        [Range(0, 1)]
        public Parameter<float> transition = 0.25f;

        public override string Info => waitUntilFinish.Value ?
            $"► Playing {stateName}" :
            $"► Play {stateName}";

        private AnimatorStateInfo stateInfo;
        private bool played;

        protected override void StartAction()
        {
            played = false;

            AnimatorStateInfo current = Agent.GetCurrentAnimatorStateInfo(layer.Value);
            Agent.CrossFade(stateName.Value, transition.Value / current.length, layer.Value);

            if (!waitUntilFinish.Value)
            {
                EndAction(true);
            }
        }

        protected override void UpdateAction()
        {
            stateInfo = Agent.GetCurrentAnimatorStateInfo(layer.Value);

            if (stateInfo.IsName(stateName.Value))
            {

                played = true;
                if (NodeRunningTime >= (stateInfo.length / Agent.speed))
                {
                    EndAction(true);
                }
            }
            else if (played)
            {
                EndAction(true);
            }
        }
    }
}