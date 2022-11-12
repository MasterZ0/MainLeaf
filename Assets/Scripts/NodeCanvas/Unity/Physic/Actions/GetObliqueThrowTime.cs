using AdventureGame.Shared.Utils;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using AdventureGame.Shared.NodeCanvas;
using HeaderAttribute = ParadoxNotion.Design.HeaderAttribute;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Rigidbody)]
    [Description("Return a Vector2 with the velocity for the oblique throw of a projectile. yLimits controls the min/max range of the throw.")]
    public class GetObliqueThrowTime : ActionTask<Rigidbody>
    {
        [Header("In")]
        public BBParameter<Vector3> targetDistance;
        public BBParameter<float> time;

        [Header("Out")]
        public BBParameter<Vector3> returnedVelocity;

        protected override void OnExecute()
        {
            returnedVelocity.value = MathUtils.ObliqueThrowTime(targetDistance.value, agent.mass, time.value);
            EndAction(true);
        }
    }
}