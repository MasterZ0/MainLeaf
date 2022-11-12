using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Animations)]
    [Description("Play animation by state name in all layers")]
    public class PlayAnimationAllLayers : ActionTask<Animator>
    {
        public BBParameter<string> stateName;
        [SliderField(0, 1)]
        public BBParameter<float> transition = 0.25f;

        public BBParameter<bool> waitUntilFinish;
        [ShowIf(nameof(waitUntilFinish), 1)]
        public BBParameter<int> waitLayer;

        protected override string info => $"► Play All: {stateName}";

        private AnimatorStateInfo stateInfo;
        private bool played;

        protected override void OnExecute()
        {
            played = false;

            for (int i = 0; i <= agent.layerCount; i++)
            {
                AnimatorStateInfo current = agent.GetCurrentAnimatorStateInfo(i);
                agent.CrossFade(stateName.value, transition.value / current.length, i);
            }

            if (!waitUntilFinish.value)
            {
                EndAction(true);
            }
        }

        protected override void OnUpdate()
        {
            stateInfo = agent.GetCurrentAnimatorStateInfo(waitLayer.value);

            if (stateInfo.IsName(stateName.value))
            {

                played = true;
                if (elapsedTime >= (stateInfo.length / agent.speed))
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