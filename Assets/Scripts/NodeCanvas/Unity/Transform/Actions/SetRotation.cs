using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{

    [Category(Categories.Transform)]
    [Description("Set Transform.position")]
    public class SetRotation : ActionTask<Transform> {

        public BBParameter<Quaternion> rotation = Quaternion.identity;

        protected override string info => $"Rotation = {rotation}";

        protected override void OnExecute() {
            agent.rotation = rotation.value;
            EndAction(true);
        }
    }
}