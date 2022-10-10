using AdventureGame.Shared.NodeCanvas;
using AdventureGame.BattleSystem;
using AdventureGame.Projectiles;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace AdventureGame.NodeCanvas.Projectiles {

    [Category(Categories.Projectiles)]
    [Description("Shoot the projectile")]
    public class ShootProjectile : ActionTask {

        [Header("In")]
        [RequiredField] public BBParameter<IAttacker> attacker;
        [RequiredField] public BBParameter<Projectile> projectile;

        [Header("Config")]
        [RequiredField] public BBParameter<DamageData> damage;
        public BBParameter<float> velocity;

        protected override string info => projectile.isDefined ?
            $"Shoot {projectile}" : name;

        protected override void OnExecute() {
            projectile.value.Shoot(new Damage(damage.value, attacker.value), velocity.value);
            EndAction(true);
        }
    }
}