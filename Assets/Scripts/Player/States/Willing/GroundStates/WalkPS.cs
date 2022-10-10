using NodeCanvas.Framework;
using UnityEngine;

namespace AdventureGame.Player.States
{
    public class WalkPS : PlayerAction
    {
        public BBParameter<float> moveSpeed;

        protected override void OnUpdate()
        {
            Physics.Move(Inputs.Move, moveSpeed.value);
            Animator.UpdateWalk();
        }
    }
}