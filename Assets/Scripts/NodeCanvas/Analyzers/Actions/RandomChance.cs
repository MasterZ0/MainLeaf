using AdventureGame.Shared.NodeCanvas;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Analyzers 
{
    [NodeCategory(Categories.Analyzers)]
    [NodeDescription("Randomizes a value from 0 to 100 and returns true based on chance value")]
    public class RandomChance : ConditionTask 
    {
        /*[RequiredField]*/ public Parameter<float> chance;

        public override string Info => $"{chance}% chance";

        public override bool CheckCondition() 
        {
            float random = Random.Range(0f, 100f);
            if (chance.Value == 0)
                return false;

            return chance.Value >= random;
        }
    }
}