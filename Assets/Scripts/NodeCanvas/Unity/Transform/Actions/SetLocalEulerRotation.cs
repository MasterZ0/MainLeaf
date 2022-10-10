using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Transform)]
    [Description("Set Transform.localRotation using Euler")]
    public class SetLocalEulerRotation : ActionTask<Transform>
    {
        public BBParameter<Vector3> rotation;

        protected override string info => $"Euler Local Rotation = {rotation}";

        protected override void OnExecute()
        {
            agent.localRotation = Quaternion.Euler(rotation.value);
            EndAction(true);
        }
    }
}