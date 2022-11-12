using AdventureGame.Shared;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureGame.BattleSystem
{
    [System.Serializable]
    public class DamageData
    {
        [MinMaxSlider(0, 300, true)]
        public Vector2Int value;
        public DamageType damageType;
        public bool canBlock;

        [SerializeReference, TypeSelection]
        public List<HitEffect> hitEffects = new List<HitEffect>();
    }
}