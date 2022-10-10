using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity 
{
    [Category(Categories.Components)]
    [Description("MonoBehaviour.enable = active")]
    public class SetActiveComponent : ActionTask<Behaviour> 
    {
        [RequiredField] public BBParameter<bool> active;

        protected override string info => active.value ? 
            $"Enable {agentInfo}" : 
            $"Disable {agentInfo}";

        protected override void OnExecute() {
            agent.enabled = active.value;
            EndAction(true);
        }
    }
}