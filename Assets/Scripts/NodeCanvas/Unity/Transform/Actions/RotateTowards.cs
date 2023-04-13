using AdventureGame.Shared.NodeCanvas;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [NodeCategory(Categories.Transform)]
    [NodeDescription("Rotate the agent towards the target per frame")]
    public class RotateTowards : ActionTask<Transform>
    {
        public Parameter<Vector3> target;
        public Parameter<float> speed = 2;
        [Range(0, 180)]
        public Parameter<float> angleDifference = 5;

        protected override void UpdateAction()
        {
            Vector3 lookPos = target.Value - Agent.position;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            Agent.rotation = Quaternion.Slerp(Agent.rotation, rotation, Time.fixedDeltaTime * speed.Value);

            if (Vector3.Angle(lookPos, Agent.forward) <= angleDifference.Value)
            {
                EndAction();
            }
        }
    }
}