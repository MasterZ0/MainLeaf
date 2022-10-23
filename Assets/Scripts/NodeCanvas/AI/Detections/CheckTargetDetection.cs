using AdventureGame.AI;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using AdventureGame.Shared.NodeCanvas;
using AdventureGame.BattleSystem;

namespace AdventureGame.NodeCanvas.AI.General { 

    [Category(Categories.AI)]
    [Description("Checks if target is inside view.")]
    public class CheckTargetDetection : ConditionTask<TargetDetection> 
    {
        public BBParameter<Transform> targetPivot;
        public BBParameter<Transform> targetCenter;
        public BBParameter<Transform> targetHead;
        
        protected override bool OnCheck() 
        {
            //The test collisions are all done inside the View classes. The node just return its Transform component.
            if (agent.FindTargetInsideRange(out Transform target))
            {
                if (target.TryGetComponent(out IBattleEntity battleEntity))
                {
                    targetCenter.value = battleEntity.Center;
                    targetPivot.value = battleEntity.Pivot;
                    targetHead.value = battleEntity.Head;
                }
                else
                {
                    targetCenter.value = target;
                    targetPivot.value = target;
                    targetHead.value = target;
                }
                return true;
            }
            return false;
        }
    }
}