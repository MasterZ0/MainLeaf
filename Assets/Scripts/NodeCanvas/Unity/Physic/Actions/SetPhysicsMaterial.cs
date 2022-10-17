using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using AdventureGame.Shared.NodeCanvas;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Physics)]
    [Description("Change the PhysicsMaterial of a Rigidbody.")]
    public class SetPhysicsMaterial : ActionTask<Collider>
    {
        [RequiredField] public BBParameter<PhysicMaterial> physicsMaterial;

        protected override string info => $"Set PhysicsMaterial to {physicsMaterial}";

        protected override void OnExecute()
        {
            agent.sharedMaterial = physicsMaterial.value;
            EndAction(true);
        }
    }
}