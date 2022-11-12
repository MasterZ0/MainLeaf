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
            velocity.value = agent.velocity;
            EndAction();
        }
    }
}