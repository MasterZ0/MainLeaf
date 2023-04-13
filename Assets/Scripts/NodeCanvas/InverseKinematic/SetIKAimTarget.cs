using AdventureGame.Shared.NodeCanvas;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using RootMotion.FinalIK;
using UnityEngine;

namespace AdventureGame.NodeCanvas.InverseKinematic
{
    //[Name("Set IK Aim Target")]
    [NodeCategory(Categories.IK)]
    [NodeDescription("TODO")]
    public class SetIKAimTarget : ActionTask<AimIK>
    {
        public Parameter<Transform> target;

        public override string Info => $"{name} = {target}";

        protected override void StartAction()
        {
            IKSolverAim ikAim = Agent.GetIKSolver() as IKSolverAim;

            ikAim.target = target.Value;
            EndAction();
        }
    }
}