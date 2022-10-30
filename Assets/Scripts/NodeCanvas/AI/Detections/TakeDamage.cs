using AdventureGame.BattleSystem;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using AdventureGame.Shared.NodeCanvas;
using UnityEngine;

namespace AdventureGame.NodeCanvas.AI
{
    [Category(Categories.AI)]
    [Description("Deals damage to IHitable")]
    public class TakeDamage : ActionTask<IHittable>
    {
        public BBParameter<Transform> sender;
        public BBParameter<int> damageValue;

        protected override string info => $"{name} = {damageValue}";
        
        protected override void OnExecute()
        {
            agent.TakeDamage(damageValue.value);
            EndAction(true);
        }
    }
}