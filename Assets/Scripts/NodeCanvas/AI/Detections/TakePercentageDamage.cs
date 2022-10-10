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
            int damageValue = Mathf.FloorToInt(agent.CurrentHealth * damagePercentage.value * 0.01f);
            Damage damage = new Damage(damageValue);
            
            agent.TakeDamage(damage);
            EndAction(true);
        }
    }
}