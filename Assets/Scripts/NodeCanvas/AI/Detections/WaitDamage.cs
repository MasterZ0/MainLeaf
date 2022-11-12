using AdventureGame.BattleSystem;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using AdventureGame.Shared.NodeCanvas;
using UnityEngine;

namespace AdventureGame.NodeCanvas.AI
{

    [Category(Categories.AI)]
    [Description("Event that is triggered when the enemy receive a damage.")]
    public class WaitDamage : ActionTask<IStatusOwner> 
    {
        public BBParameter<Transform> senderPivot;
        public BBParameter<Transform> senderCenter;
        public BBParameter<Transform> senderHead;

        protected override void OnExecute() 
        {
            agent.Status.OnTakeDamage += OnTakeDamage;
        }

        protected override void OnStop()
        {
            agent.Status.OnTakeDamage -= OnTakeDamage;
        }

        public void OnTakeDamage(DamageInfo damageInfo)
        {
            if (damageInfo.Sender != null)
            {
                senderPivot.value = damageInfo.Sender.Pivot;
                senderCenter.value = damageInfo.Sender.Center;
                senderHead.value = damageInfo.Sender.Head;
            }
            EndAction(true);
        }
    }
}