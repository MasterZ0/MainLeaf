using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using AdventureGame.Shared.NodeCanvas;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Rigidbody)]
    [Description("Set the torque of a Rigidbody.")]
    public class AddTorque : ActionTask<Rigidbody>
    {
        public BBParameter<Vector3> torque;
        public BBParameter<ForceMode> forceMode;

        protected override string info => $"Add Torque = {torque}";

        protected override void OnExecute()
        {
            agent.AddTorque(torque.value, forceMode.value);
            EndAction(true);
        }        
    }
}