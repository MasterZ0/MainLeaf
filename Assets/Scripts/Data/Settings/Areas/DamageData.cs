using AdventureGame.Shared;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AdventureGame.BattleSystem
{
    [System.Serializable]
    public class DamageData
    {
        [MinMaxSlider(0, 300, true)]
        public Vector2Int Value;
        public DamageType DamageType;
        public bool CanBlock;

        //[SerializeReference, TypeSelection]
        //public List<HitEffect> HitEffects = new List<HitEffect>();
    }
}