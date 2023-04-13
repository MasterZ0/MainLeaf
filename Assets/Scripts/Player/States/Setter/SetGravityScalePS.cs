using Z3.NodeGraph.Core;
using UnityEngine;

namespace AdventureGame.Player.States
{
    public class SetGravityScalePS : PlayerAction 
    {
        public Parameter<float> gravityScale;

        protected override void EnterState()
        {
            Physics.SetGravityScale(gravityScale.Value);
            EndAction();
        }
    }
}
