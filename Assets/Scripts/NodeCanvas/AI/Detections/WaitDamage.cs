using AdventureGame.BattleSystem;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using AdventureGame.Shared.NodeCanvas;

namespace AdventureGame.NodeCanvas.AI
{

    [Category(Categories.AI)]
    [Description("Event that is triggered when the enemy receive a damage.")]
    public class WaitDamage : ActionTask<IHittable> {

        protected override void OnExecute() {
            agent.OnTakeDamage += OnTakeDamage;
        }

        protected override void OnStop()
        {
            agent.OnTakeDamage -= OnTakeDamage;
        }

        public void OnTakeDamage(DamageInfo damageInfo) {
            EndAction(true);
        }
    }
}