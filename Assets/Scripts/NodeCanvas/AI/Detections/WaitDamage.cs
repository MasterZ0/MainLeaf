using AdventureGame.BattleSystem;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using AdventureGame.Shared.NodeCanvas;
using UnityEngine;

namespace AdventureGame.NodeCanvas.AI
{

    [NodeCategory(Categories.AI)]
    [NodeDescription("Event that is triggered when the enemy receive a damage.")]
    public class WaitDamage : ActionTask<IStatusOwner> 
    {
        public Parameter<Transform> senderPivot;
        public Parameter<Transform> senderCenter;
        public Parameter<Transform> senderHead;

        protected override void StartAction() 
        {
            Agent.Status.OnTakeDamage += OnTakeDamage;
        }

        protected override void StopAction()
        {
            Agent.Status.OnTakeDamage -= OnTakeDamage;
        }

        public void OnTakeDamage(DamageInfo damageInfo)
        {
            if (damageInfo.Sender != null)
            {
                senderPivot.Value = damageInfo.Sender.Pivot;
                senderCenter.Value = damageInfo.Sender.Center;
                senderHead.Value = damageInfo.Sender.Head;
            }
            EndAction(true);
        }
    }
}