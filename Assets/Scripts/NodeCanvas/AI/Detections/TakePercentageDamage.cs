using AdventureGame.BattleSystem;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using AdventureGame.Shared.NodeCanvas;
using UnityEngine;

namespace AdventureGame.NodeCanvas.AI
{
    [NodeCategory(Categories.AI)]
    [NodeDescription("Deals damage to an IDamageable based on a percentage")]
    public class TakePercentageDamage : ActionTask<IStatusOwner>
    {
        public Parameter<Transform> sender;
        public Parameter<float> damagePercentage;

        public override string Info => $"Take {damagePercentage.Value}% Damage";
        
        protected override void StartAction()
        {
            Agent.TakeDamagePercentage(damagePercentage.Value);
            EndAction(true);
        }
    }
}