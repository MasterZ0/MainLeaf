using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using HeaderAttribute = ParadoxNotion.Design.HeaderAttribute;

namespace AdventureGame.NodeCanvas.Unity.Physic
{
    [Category(Categories.Rigidbody)]
    [Description("Clamp Velocity to a set of values")]
    public class ClampVelocity : ActionTask<Rigidbody>
    {
        [Header("Inputs")]
        public BBParameter<Vector2> range;
        protected override string info => $"Clamp Velocity, Range: {range}";
        protected override void OnExecute()
        {
            agent.velocity = new Vector3()
            {
                x = Mathf.Clamp(agent.velocity.x, range.value.x, range.value.y),
                y = Mathf.Clamp(agent.velocity.y, range.value.x, range.value.y),
                z = Mathf.Clamp(agent.velocity.z, range.value.x, range.value.y)
            };
            EndAction(true);
        }
    }
}