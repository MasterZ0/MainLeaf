using UnityEngine;

namespace AdventureGame.BattleSystem
{
    public interface IBattleEntity
    {
        // Name?
        public Transform Center { get; }
        public Transform Pivot { get; }
    }
}