using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity { 

    [Category(Categories.Transform)]
    [Description("Set transform.localScale")]
    public class SetScale : ActionTask<Transform> {

        public BBParameter<Vector3> scale;

        protected override void OnExecute() {
            agent.localScale = scale.value;
            EndAction(true);
        }
    }
}