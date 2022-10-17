using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity.Physic
{
    [Category(Categories.Rigidbody)]
    [Description("Moves a rigidbody in the Transform.Forward direction. This movement preserves the current speed in Y.")]
    public class ForwardMovement : ActionTask<Rigidbody>
    {
        public BBParameter<float> speed;
        protected override string info => $"Forward Movement = {speed}";
        protected override void OnExecute()
        {
            Vector3 forward = agent.transform.forward * speed.value;
            agent.velocity = new Vector3(forward.x, agent.velocity.y, forward.z);
            EndAction(true);
        }
    }
}