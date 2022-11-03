namespace AdventureGame.Player.States
{
    public class CheckGroundPS : PlayerCondition
    {
        protected override bool OnCheck() => Physics.CheckGround();
    }
}
