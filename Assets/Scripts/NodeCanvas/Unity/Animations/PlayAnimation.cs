using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Animations)]
    [Description("Play animation by state name")]
    public class PlayAnimation : ActionTask<Animator> 
    {
        public BBParameter<bool> waitUntilFinish;
        public BBParameter<string> stateName;
        public BBParameter<int> layer;
        [SliderField(0, 1)]
        public BBParameter<float> transition = 0.25f;

        protected override string info => waitUntilFinish.value ?
            $"► Playing {stateName}" :
            $"► Play {stateName}";

        private AnimatorStateInfo stateInfo;
        private bool played;

        protected override void OnExecute() 
        {
            played = false;

            AnimatorStateInfo current = agent.GetCurrentAnimatorStateInfo(layer.value);
            agent.CrossFade(stateName.value, transition.value / current.length, layer.value);

            if (!waitUntilFinish.value) {
                EndAction(true);
            }
        }

        protected override void OnUpdate() 
        {
            stateInfo = agent.GetCurrentAnimatorStateInfo(layer.value);

            if (stateInfo.IsName(stateName.value)) {

                played = true;
                if (elapsedTime >= (stateInfo.length / agent.speed)) {
                    EndAction(true);
                }
            }
            else if (played) {
                EndAction(true);
            }
        }
    }
}