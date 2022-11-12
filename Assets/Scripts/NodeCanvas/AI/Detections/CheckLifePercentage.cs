using NodeCanvas.Framework;
using ParadoxNotion;
using ParadoxNotion.Design;
using AdventureGame.Shared.NodeCanvas;
using AdventureGame.BattleSystem;

namespace AdventureGame.NodeCanvas.AI 
{
    [Category(Categories.AI)]
    [Description("Compare the current health")]
    public class CheckLifePercentage : ConditionTask<IStatusOwner> 
    {
        [SliderField(0, 100)]
        public BBParameter<float> percentage;
        public CompareMethod checkType = CompareMethod.LessOrEqualTo;

        protected override string info => "Health Percentage" + OperationTools.GetCompareString(checkType) + percentage;
        
        protected override bool OnCheck() 
        {
            float healthPercentage = agent.GetAttributes().HPPercentage();
            return OperationTools.Compare(healthPercentage, percentage.value / 100f, checkType, 0f);
        }
    }
}