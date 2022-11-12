using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using RootMotion.FinalIK;
using UnityEngine;

namespace AdventureGame.NodeCanvas.InverseKinematic
{
    [Name("Set IK Look At Target")]
    [Category(Categories.IK)]
    [Description("TODO")]
    public class SetIKLookAtTarget : ActionTask<LookAtIK>
    {
        public BBParameter<Transform> target;

        protected override string info => $"{name} = {target}";

        protected override void OnExecute()
        {
            IKSolverLookAt ikLookAt = agent.GetIKSolver() as IKSolverLookAt;

            ikLookAt.target = target.value;
            EndAction();
        }
    }
}