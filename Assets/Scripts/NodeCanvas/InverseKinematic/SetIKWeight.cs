using AdventureGame.Shared.NodeCanvas;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using RootMotion.FinalIK;
using UnityEngine;

namespace AdventureGame.NodeCanvas.InverseKinematic
{
    //[Name("Set IK Weight")]
    [NodeCategory(Categories.IK)]
    [NodeDescription("TODO")]
    public class SetIKWeight : ActionTask<IK>
    {
        public bool useSpeed = true;
        //[Range(0f, 1f)]
        public Parameter<float> weight;
        ////[ShowIf(nameof(useSpeed), 1)]
        public Parameter<float> duration;

        private float currentWeight;

        //public override string Info => $"Set {AgentInfo} Weight = {weight}";

        protected override void StartAction()
        {
            if (useSpeed)
            {
                currentWeight = Agent.GetIKSolver().GetIKPositionWeight();
            }
            else
            {
                Agent.GetIKSolver().SetIKPositionWeight(weight.Value);
                EndAction();
            }
        }

        protected override void UpdateAction()
        {
            currentWeight = Mathf.MoveTowards(currentWeight, weight.Value, 1f / duration.Value * Time.fixedDeltaTime);
            Agent.GetIKSolver().SetIKPositionWeight(currentWeight);

            if (currentWeight == weight.Value)
            {
                EndAction();
            }
        }
    }
}