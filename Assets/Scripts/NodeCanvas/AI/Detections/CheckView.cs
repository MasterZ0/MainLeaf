using AdventureGame.AI;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using AdventureGame.Shared.NodeCanvas;

namespace AdventureGame.NodeCanvas.AI.General { 

    [Category(Categories.AI)]
    [Description("Checks if target is inside view.")]
    public class CheckView : ConditionTask<ViewDetection> {

        public BBParameter<Transform> target;
        
        protected override bool OnCheck() {

            //The test collisions are all done inside the View classes. The node just return its Transform component.
            if (agent.FindTargetInsideRange(out Transform targetTemp))
            {
                target.value = targetTemp;
                return true;
            }
            return false;
        }
    }
}