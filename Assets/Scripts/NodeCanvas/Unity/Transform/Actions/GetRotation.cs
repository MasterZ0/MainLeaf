using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Transform)]
    [Description("Get the transform.rotation.")]
    public class GetRotation : ActionTask<Transform>
    {
        [ParadoxNotion.Design.Header("Out")]
        public BBParameter<Quaternion> rotation;

        protected override string info => $"Get {agentInfo} Rotation";
        protected override void OnExecute()
        {
            rotation.value = agent.rotation;
            EndAction(true);
        }
    }
}