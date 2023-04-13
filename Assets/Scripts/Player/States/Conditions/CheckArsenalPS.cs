using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;

namespace AdventureGame.Player.States
{
    [NodeDescription("Used to evaluate the equipped items")]
    public class CheckArsenalPS : PlayerCondition
    {
        public enum ArsenalCondition
        {
            CanShootArrow
        }

        public Parameter<ArsenalCondition> condition;

        public override bool CheckCondition()
        {
            return condition.Value switch
            {
                ArsenalCondition.CanShootArrow => Arsenal.CanShootArrow,
                _ => throw new System.NotImplementedException(),
            };
        }
    }
}