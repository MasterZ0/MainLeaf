using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Physics)]
    [Description("Get Character Controller Velocity")]
    public class GetCharacterControllerVelocity : ActionTask<CharacterController>
    {
        public BBParameter<Vector3> velocity;

        protected override string info => $"Get {agentInfo} Velocity";

        protected override void OnExecute()
        {
            Transform transform = agent.transform;
            Vector3 characterControllerVelocity = agent.velocity;

            velocity.value = new Vector3()
            {
                x = Vector3.Dot(transform.right, characterControllerVelocity),
                y = Vector3.Dot(transform.up, characterControllerVelocity),
                z = Vector3.Dot(transform.forward, characterControllerVelocity)
            };

            EndAction();
        }
    }
}