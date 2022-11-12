namespace AdventureGame.BattleSystem
{
    public interface IStatusOwner : IBattleEntity 
    {
        IStatusController Status { get; }
    }
}