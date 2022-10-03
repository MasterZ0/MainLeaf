namespace AdventureGame.BattleSystem
{
    /// <summary>
    /// Anything that can die
    /// </summary>
    public interface IDamageable : IHittable
    {
        int MaxHealth { get; }
        int CurrentHealth { get; }
    }
}