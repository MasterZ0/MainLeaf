using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity {

    [Category(Categories.Transform)]
    [Description("Set Transform.position")]
    public class SetPosition : ActionTask<Transform> {

        public BBParameter<Vector3> position;

        protected override string info => $"Position = {position}";

        protected override void OnExecute() {
            agent.position = position.value;
            EndAction(true);
        }
    }
}