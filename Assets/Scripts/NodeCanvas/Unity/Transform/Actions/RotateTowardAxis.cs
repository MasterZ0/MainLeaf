using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity 
{
    [Category(Categories.Transform)]
    [Description("Rotate Z axis of a GameObject")]
    public class RotateTowardAxis : ActionTask<Transform> 
    {       
        public BBParameter<float> degrees;
        public BBParameter<float> speed;

        protected override string info => $"Rotate {agentInfo} {degrees} degrees";

        private float currentDegrees;
        private float initialAngle;
        protected override void OnExecute() 
        {
            currentDegrees = 0;
            initialAngle = agent.eulerAngles.z;
        }

        protected override void OnUpdate() 
        {
            currentDegrees = Mathf.MoveTowards(currentDegrees, degrees.value, Time.fixedDeltaTime * speed.value);
            agent.rotation = Quaternion.Euler(agent.eulerAngles.x, agent.eulerAngles.y, currentDegrees + initialAngle);
            
            if (currentDegrees == degrees.value) 
            {
                EndAction(true);
            }
        }
    }
}