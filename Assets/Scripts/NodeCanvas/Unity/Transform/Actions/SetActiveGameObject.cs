using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Transform)]
    [Description("GameObject.SetActive(active)")]
    public class SetActiveGameObject : ActionTask<Transform> 
    {
        [RequiredField] public BBParameter<bool> active;

        protected override string info => active.value ?
            $"Enable {agentInfo}" : $"Disable {agentInfo}";

        protected override void OnExecute() {
            agent.gameObject.SetActive(active.value);
            EndAction(true);
        }
    }
}