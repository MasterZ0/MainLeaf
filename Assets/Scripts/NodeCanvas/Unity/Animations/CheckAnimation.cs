using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Animations)]
    [Description("Check the current animation by state name")]
    public class CheckAnimation : ConditionTask<Animator>
    {
        public BBParameter<string> stateName;

        protected override string info => $"Animation == {stateName}";

        protected override bool OnCheck()
        {
            var stateInfo = agent.GetCurrentAnimatorStateInfo(0);
            return stateInfo.IsName(stateName.value);
        }
    }
}