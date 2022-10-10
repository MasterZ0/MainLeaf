using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Transform)]
    [Description("Set Transform.rotation using Euler")]
    public class SetEulerRotation : ActionTask<Transform>
    {
        public BBParameter<Vector3> rotation;

        protected override string info => $"Euler Rotation = {rotation}";

        protected override void OnExecute()
        {
            agent.rotation = Quaternion.Euler(rotation.value);
            EndAction(true);
        }
    }
}