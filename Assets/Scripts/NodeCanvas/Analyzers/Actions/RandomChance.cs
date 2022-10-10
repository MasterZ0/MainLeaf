using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Analyzers 
{
    [Category(Categories.Analyzers)]
    [Description("Randomizes a value from 0 to 100 and returns true based on chance value")]
    public class RandomChance : ConditionTask 
    {
        [RequiredField] public BBParameter<float> chance;

        protected override string info => $"{chance}% chance";

        protected override bool OnCheck() 
        {
            float random = Random.Range(0f, 100f);
            if (chance.value == 0)
                return false;

            return chance.value >= random;
        }
    }
}