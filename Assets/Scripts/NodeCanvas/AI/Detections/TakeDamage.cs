using AdventureGame.BattleSystem;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using AdventureGame.Shared.NodeCanvas;
using UnityEngine;

namespace AdventureGame.NodeCanvas.AI
{
    [NodeCategory(Categories.AI)]
    [NodeDescription("Deals damage to IHitable")]
    public class TakeDamage : ActionTask<IStatusOwner>
    {
        public Parameter<Transform> sender;
        public Parameter<int> damageValue;

        public override string Info => $"{name} = {damageValue}";
        
        protected override void StartAction()
        {
            Agent.TakeDamage(damageValue.Value);
            EndAction(true);
        }
    }
}