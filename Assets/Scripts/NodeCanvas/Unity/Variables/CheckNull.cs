using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Variables)]
    [Description("Best way to check if some object is null")]
    public class CheckNull : ConditionTask 
    {
        [BlackboardOnly]
        public BBParameter<object> variable;

        protected override string info {
            get { return variable + " == null"; }
        }

        protected override bool OnCheck() {
            return variable.isNull;
        }
    }
}