using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using AdventureGame.Shared.NodeCanvas;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Rigidbody2D)]
    [Description("Set the torque of a Rigidbody2D.")]
    public class AddTorque : ActionTask<Rigidbody2D>
    {
        public BBParameter<float> torque;
        public BBParameter<ForceMode2D> forceMode2D;

        protected override string info => $"Add Torque = {torque}";

        protected override void OnExecute()
        {
            agent.AddTorque(torque.value, forceMode2D.value);
            EndAction(true);
        }        
    }
}