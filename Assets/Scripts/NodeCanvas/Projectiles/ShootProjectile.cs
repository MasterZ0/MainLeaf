using AdventureGame.Shared.NodeCanvas;
using AdventureGame.BattleSystem;
using AdventureGame.Projectiles;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Projectiles {

    [NodeCategory(Categories.Projectiles)]
    [NodeDescription("Shoot the projectile")]
    public class ShootProjectile : ActionTask<IStatusOwner> 
    {
        [Header("In")]
        /*[RequiredField]*/ public Parameter<Projectile> projectile;

        [Header("Config")]
        /*[RequiredField]*/ public Parameter<DamageData> damage;
        public Parameter<float> velocity;

        //public override string Info => projectile.isDefined ?
        //    $"Shoot {projectile}" : name;

        protected override void StartAction() 
        {
            projectile.Value.Shoot(new Damage(damage.Value, Agent), velocity.Value);
            EndAction(true);
        }
    }
}