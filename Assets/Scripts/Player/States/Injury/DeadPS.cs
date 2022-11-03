using AdventureGame.Shared;
using UnityEngine;

namespace AdventureGame.Player.States
{
    public class DeadPS : PlayerAction
    {
        private readonly Timer deadTimer = new Timer();

        #region Action
        protected override void EnterState()
        {
            //Physics.SetVelocity(Vector2.zero);
            //Animator.Dead();
            //Physics.FullFriction();

            deadTimer.Set(2);
            //deadTimer.Set(VisualSettings.TimeToShowGameOver); // TODO: Improve
            deadTimer.OnCompleted += Status.GameOver;
        }

        protected override void OnUpdate()
        {
            deadTimer.FixedTick();
        }
        #endregion
    }
}