using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Transform)]
    [Description("Set the Transform Parent")]
    public class SetParent : ActionTask<Component>
    {
        public BBParameter<Transform> parent;
        protected override string info => $"Parent = {parent}";
        protected override void OnExecute()
        {
            agent.transform.SetParent(parent.value);
            EndAction(true);
        }
    }
}