using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using AdventureGame.Shared.NodeCanvas;
using AdventureGame.BattleSystem;
using UnityEngine;

namespace AdventureGame.NodeCanvas.AI 
{
    [NodeCategory(Categories.AI)]
    [NodeDescription("Compare the current health")]
    public class CheckLifePercentage : ConditionTask<IStatusOwner> 
    {
        [Range(0, 100)]
        public Parameter<float> percentage;
        public CompareMethod checkType = CompareMethod.LessOrEqualTo;

        public override string Info => "Health Percentage" + OperationTools.GetCompareString(checkType) + percentage;
        
        public override bool CheckCondition() 
        {

            float healthPercentage = Agent.GetAttributes().HPPercentage();
            return OperationTools.Compare(healthPercentage, percentage.Value / 100f, checkType, 0f);
        }
    }
}