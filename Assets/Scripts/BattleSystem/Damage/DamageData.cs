using AdventureGame.Shared;
using System.Collections.Generic;
using UnityEngine;
using Z3.UIBuilder.Core;

namespace AdventureGame.BattleSystem
{
    [System.Serializable]
    public class DamageData
    {
        //[MinMaxSlider(0, 300, true)]
        public Vector2Int value;
        public DamageType damageType;
        public bool canBlock;
        public bool showHitParticle = true;

        [SerializeReference, TypeSelection]
        public List<HitEffect> hitEffects = new List<HitEffect>();
    }
}