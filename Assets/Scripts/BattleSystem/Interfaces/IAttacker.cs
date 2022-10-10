namespace AdventureGame.BattleSystem
{
    public interface IAttacker
    {
        // AttackerInfo: Name, position, atributes?, etc... 
        void OnDamageDealt(DamageInfo info);
    }
}