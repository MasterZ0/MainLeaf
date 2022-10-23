using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using RootMotion.FinalIK;
using UnityEngine;

namespace AdventureGame.NodeCanvas.InverseKinematic
{
    [Name("Set IK Weight")]
    [Category(Categories.IK)]
    [Description("TODO")]
    public class SetIKWeight : ActionTask<IK>
    {
        public bool useSpeed = true;
        [SliderField(0f, 1f)]
        public BBParameter<float> weight;
        [ShowIf(nameof(useSpeed), 1)]
        public BBParameter<float> duration;

        private float currentWeight;

        protected override string info => $"Set {agentInfo} Weight = {weight}";

        protected override void OnExecute()
        {
            if (useSpeed)
            {
                currentWeight = agent.GetIKSolver().GetIKPositionWeight();
            }
            else
            {
                agent.GetIKSolver().SetIKPositionWeight(weight.value);
                EndAction();
            }
        }

        protected override void OnUpdate()
        {
            currentWeight = Mathf.MoveTowards(currentWeight, weight.value, (1f / duration.value) * Time.fixedDeltaTime);
            agent.GetIKSolver().SetIKPositionWeight(currentWeight);

            if (currentWeight == weight.value)
            {
                EndAction();
            }
        }
    }
}