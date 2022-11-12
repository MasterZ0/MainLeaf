using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using RootMotion.FinalIK;
using UnityEngine;

namespace AdventureGame.NodeCanvas.InverseKinematic
{
    [Name("Set IK Aim Target")]
    [Category(Categories.IK)]
    [Description("TODO")]
    public class SetIKAimTarget : ActionTask<AimIK>
    {
        public BBParameter<Transform> target;

        protected override string info => $"{name} = {target}";

        protected override void OnExecute()
        {
            IKSolverAim ikAim = agent.GetIKSolver() as IKSolverAim;

            ikAim.target = target.value;
            EndAction();
        }
    }
}