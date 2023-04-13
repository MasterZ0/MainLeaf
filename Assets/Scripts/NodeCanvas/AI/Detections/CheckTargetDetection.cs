using AdventureGame.AI;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using UnityEngine;
using AdventureGame.Shared.NodeCanvas;
using AdventureGame.BattleSystem;

namespace AdventureGame.NodeCanvas.AI.General { 

    [NodeCategory(Categories.AI)]
    [NodeDescription("Checks if target is inside view.")]
    public class CheckTargetDetection : ConditionTask<TargetDetection> 
    {
        public Parameter<Transform> targetPivot;
        public Parameter<Transform> targetCenter;
        public Parameter<Transform> targetHead;
        
        public override bool CheckCondition() 
        {
            //The test collisions are all done inside the View classes. The node just return its Transform component.
            if (Agent.FindTargetInsideRange(out Transform target))
            {
                if (target.TryGetComponent(out IBattleEntity battleEntity))
                {
                    targetCenter.Value = battleEntity.Center;
                    targetPivot.Value = battleEntity.Pivot;
                    targetHead.Value = battleEntity.Head;
                }
                else
                {
                    targetCenter.Value = target;
                    targetPivot.Value = target;
                    targetHead.Value = target;
                }
                return true;
            }
            return false;
        }
    }
}