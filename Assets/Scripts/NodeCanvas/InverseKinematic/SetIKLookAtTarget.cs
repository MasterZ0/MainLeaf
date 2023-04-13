using AdventureGame.Shared.NodeCanvas;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using RootMotion.FinalIK;
using UnityEngine;

namespace AdventureGame.NodeCanvas.InverseKinematic
{
    //[Name("Set IK Look At Target")]
    [NodeCategory(Categories.IK)]
    [NodeDescription("TODO")]
    public class SetIKLookAtTarget : ActionTask<LookAtIK>
    {
        public Parameter<Transform> target;

        public override string Info => $"{name} = {target}";

        protected override void StartAction()
        {
            IKSolverLookAt ikLookAt = Agent.GetIKSolver() as IKSolverLookAt;

            ikLookAt.target = target.Value;
            EndAction();
        }
    }
}