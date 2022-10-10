using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    public class PlayAnimationAllLayers : ActionTask<Animator>
    {
        public BBParameter<string> stateName;
        public BBParameter<int> layerCount = 1;
        [SliderField(0, 1)]
        public BBParameter<float> transition = 0.25f;

        protected override string info => $"► Play All: {stateName}";

        protected override void OnExecute()
        {
            for (int i = 0; i <= layerCount.value; i++)
            {
                AnimatorStateInfo current = agent.GetCurrentAnimatorStateInfo(i);
                agent.CrossFade(stateName.value, transition.value / current.length, i);
            }
            EndAction();
        }
    }
}