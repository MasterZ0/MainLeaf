using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace AdventureGame.Player.States
{
    [Description("Used to evaluate the equipped items")]
    public class CheckArsenalPS : PlayerCondition
    {
        public enum ArsenalCondition
        {
            CanShootArrow
        }

        public BBParameter<ArsenalCondition> condition;

        protected override bool OnCheck()
        {
            return condition.value switch
            {
                ArsenalCondition.CanShootArrow => Arsenal.CanShootArrow,
                _ => throw new System.NotImplementedException(),
            };
        }
    }
}