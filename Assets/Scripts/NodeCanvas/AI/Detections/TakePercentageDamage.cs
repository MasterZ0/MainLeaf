using AdventureGame.BattleSystem;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using AdventureGame.Shared.NodeCanvas;
using UnityEngine;

namespace AdventureGame.NodeCanvas.AI
{
    [Category(Categories.AI)]
    [Description("Deals damage to an IDamageable based on a percentage")]
    public class TakePercentageDamage : ActionTask<IDamageable>
    {
        public BBParameter<Transform> sender;
        public BBParameter<float> damagePercentage;

        protected override string info => $"Take {damagePercentage.value}% Damage";
        
        protected override void OnExecute()
        {
            agent.TakeDamagePercentage(damagePercentage.value);
            EndAction(true);
        }
    }
}