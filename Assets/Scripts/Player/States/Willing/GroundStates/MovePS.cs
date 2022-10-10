using NodeCanvas.Framework;
using UnityEngine;

namespace AdventureGame.Player.States
{
    public class MovePS : PlayerAction 
    {
        public BBParameter<float> moveSpeed;
        public BBParameter<float> maxVelocityScale;

        protected override void OnUpdate()
        {
            Physics.Move(Inputs.Move, moveSpeed.value);
            Animator.UpdateRunBody();
        }
    }
}