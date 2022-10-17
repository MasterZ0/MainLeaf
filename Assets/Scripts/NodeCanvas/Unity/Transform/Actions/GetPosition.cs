using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Transform)]
    [Description("Get the transform.position.")]
    public class GetPosition : ActionTask<Transform> 
    {
        [ParadoxNotion.Design.Header("Out")]
        public BBParameter<Vector3> position;

        protected override string info => $"Get {agentInfo} Position";
        protected override void OnExecute() {
            position.value = agent.position;
            EndAction(true);
        }
    }
}