using Z3.NodeGraph.Core;

namespace AdventureGame.Player.States
{
    public class MovePS : PlayerAction 
    {
        public Parameter<float> moveSpeed;

        protected override void UpdateAction()
        {
            Physics.Move(moveSpeed.Value);
        }
    }
}