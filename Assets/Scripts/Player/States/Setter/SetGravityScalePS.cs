using NodeCanvas.Framework;
using UnityEngine;

namespace AdventureGame.Player.States
{
    public class SetGravityScalePS : PlayerAction 
    {
        public BBParameter<float> gravityScale;

        protected override void EnterState()
        {
            Physics.SetGravityScale(gravityScale.value);
            EndAction();
        }
    }
}
