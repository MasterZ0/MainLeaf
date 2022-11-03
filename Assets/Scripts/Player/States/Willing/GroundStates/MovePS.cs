using NodeCanvas.Framework;

namespace AdventureGame.Player.States
{
    public class MovePS : PlayerAction 
    {
        public BBParameter<float> moveSpeed;

        protected override void OnUpdate()
        {
            Physics.Move(moveSpeed.value);
        }
    }
}