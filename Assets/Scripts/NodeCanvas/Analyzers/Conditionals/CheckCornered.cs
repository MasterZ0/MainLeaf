using AdventureGame.Shared.NodeCanvas;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Analyzers 
{
    [NodeCategory(Categories.Analyzers)]
    [NodeDescription("Check for short space between limits and target.")]
    public class CheckCornered : ConditionTask<Transform> 
    {
        public Parameter<Vector3> target;
        public Parameter<Vector3> leftLimit;
        public Parameter<Vector3> rightLimit;
        public Parameter<float> minimumDistance;

        public override bool CheckCondition() 
        {
            if (Mathf.Abs(Agent.position.x - target.Value.x) < minimumDistance.Value)
            {
                bool targetRight = Agent.position.x < target.Value.x;

                if (targetRight)    
                {
                    // Cornered on the left
                    return (Mathf.Abs(Agent.position.x - leftLimit.Value.x) < minimumDistance.Value);
                }
                else
                {
                    // Cornered on the right
                    return (Mathf.Abs(Agent.position.x - rightLimit.Value.x) < minimumDistance.Value);
                }
            }

            return false;
        }
    }
}