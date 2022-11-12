using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Transform)]
    [Description("Rotate the agent towards the target per frame")]
    public class RotateTowards : ActionTask<Transform>
    {
        public BBParameter<Vector3> target;
        public BBParameter<float> speed = 2;
        [SliderField(0, 180)]
        public BBParameter<float> angleDifference = 5;

        protected override void OnUpdate()
        {
            Vector3 lookPos = target.value - agent.position;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            agent.rotation = Quaternion.Slerp(agent.rotation, rotation, Time.fixedDeltaTime * speed.value);

            if (Vector3.Angle(lookPos, agent.forward) <= angleDifference.value)
            {
                EndAction();
            }
        }
    }
}