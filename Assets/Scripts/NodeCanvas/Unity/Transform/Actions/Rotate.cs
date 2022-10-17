using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity {

    [Category(Categories.Transform)]
    [Description("transform.Rotation(euler)")]
    public class Rotate : ActionTask<Transform> {

        public BBParameter<Vector3> eulerAngles;

        protected override string info => $"Rotate {eulerAngles}";

        protected override void OnExecute() {
            agent.Rotate(eulerAngles.value);
            EndAction(true);
        }
    }
}