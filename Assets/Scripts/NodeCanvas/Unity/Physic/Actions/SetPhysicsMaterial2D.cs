using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using AdventureGame.Shared.NodeCanvas;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Rigidbody2D)]
    [Description("Change the PhysicsMaterial2D of a Rigidbody2D.")]
    public class SetPhysicsMaterial2D : ActionTask<Rigidbody2D>
    {
        [RequiredField] public BBParameter<PhysicsMaterial2D> physicsMaterial2D;

        protected override string info => $"Set PhysicsMaterial2D to {physicsMaterial2D}";

        protected override void OnExecute()
        {
            agent.sharedMaterial = physicsMaterial2D.value;
            EndAction(true);
        }
    }
}