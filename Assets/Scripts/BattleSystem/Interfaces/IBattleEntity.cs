using UnityEngine;

namespace AdventureGame.BattleSystem
{
    public interface IBattleEntity // Center and Head could be a humanoid interface
    {
        // Name? 
        /// <summary> Used to get components </summary>
        public Transform Pivot { get; }
        public Transform Head { get; }
        public Transform Center { get; }
    }
}