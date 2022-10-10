using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Transform)]
    [Description("Return the parent's transform from the agent.")]
    public class GetParent : ActionTask <Transform>
    {
        public BBParameter<Transform> returnParent;
        protected override void OnExecute() {
            returnParent.value = agent.parent;
            EndAction(true);
        }
    }
}