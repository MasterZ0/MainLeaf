using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity {

    [Category(Categories.Transform)]
    [Description("Rotate rotation")]
    public class Rotate : ActionTask<Transform> {

        public BBParameter<Vector3> angle;

        protected override string info => $"Rotate {angle}";

        protected override void OnExecute() {
            agent.Rotate(angle.value);
            EndAction(true);
        }
    }
}