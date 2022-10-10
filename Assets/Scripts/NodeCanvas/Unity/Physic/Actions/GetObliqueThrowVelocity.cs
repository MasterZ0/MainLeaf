using AdventureGame.Shared.Utils;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using AdventureGame.Shared.NodeCanvas;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Rigidbody2D)]
    [Description("Return a Vector2 with the velocity for the oblique throw of a projectile. yLimits controls the min/max range of the throw.")]
    public class GetObliqueThrowVelocity : ActionTask<Rigidbody2D>
    {
        [RequiredField] public BBParameter<Vector2> targetDistance;
        [RequiredField] public BBParameter<float> speedX;
        public BBParameter<Vector2> speedYLimits;
        [RequiredField] public BBParameter<Vector2> returnedVelocity;

        protected override void OnExecute()
        {
            returnedVelocity.value = MathUtils.ObliqueThrowX(targetDistance.value, agent.gravityScale, 
                                                             speedX.value, speedYLimits.value);
            EndAction(true);
        }
    }
}