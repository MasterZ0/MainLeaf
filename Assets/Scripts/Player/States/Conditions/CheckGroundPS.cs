namespace AdventureGame.Player.States
{
    public class CheckGroundPS : PlayerCondition
    {
        public override bool CheckCondition() => Physics.CheckGround();
    }
}
