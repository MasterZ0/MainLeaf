using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Analyzers 
{
    [Category(Categories.Analyzers)]
    [Description("Check for short space between limits and target.")]
    public class CheckCornered : ConditionTask<Transform> 
    {
        public BBParameter<Vector3> target;
        public BBParameter<Vector3> leftLimit;
        public BBParameter<Vector3> rightLimit;
        public BBParameter<float> minimumDistance;

        protected override bool OnCheck() 
        {
            if (Mathf.Abs(agent.position.x - target.value.x) < minimumDistance.value)
            {
                bool targetRight = agent.position.x < target.value.x;

                if (targetRight)    
                {
                    // Cornered on the left
                    return (Mathf.Abs(agent.position.x - leftLimit.value.x) < minimumDistance.value);
                }
                else
                {
                    // Cornered on the right
                    return (Mathf.Abs(agent.position.x - rightLimit.value.x) < minimumDistance.value);
                }
            }

            return false;
        }
    }
}