using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Transform)]
    [Description("Rotate axis of a GameObject")]
    public class RotateToTargetOnAxis : ActionTask<Transform> 
    {       
        public BBParameter<Axis> axis = Axis.Y;
        public BBParameter<Vector3> target;
        public BBParameter<float> speed;
        [SliderField(0, 180)]
        public BBParameter<float> angleDifference = 10f;

        protected override string info => $"Rotate {axis} To {target}";

        private Func<Vector3> getLookDirection;

        protected override void OnExecute() 
        {
            getLookDirection = () => axis.value switch
            {
                Axis.X => new Vector3(0f, target.value.y - agent.position.y, target.value.z - agent.position.z),
                Axis.Y => new Vector3(target.value.x - agent.position.x, 0f, target.value.z - agent.position.z),
                Axis.Z => new Vector3(target.value.x - agent.position.x, target.value.y - agent.position.y, 0f),
                _ => throw new NotImplementedException(),
            };
        }

        protected override void OnUpdate() 
        {
            Vector3 lookDirection = getLookDirection();
            Quaternion rotation = Quaternion.LookRotation(lookDirection);
            agent.rotation = Quaternion.Slerp(agent.rotation, rotation, Time.fixedDeltaTime * speed.value);

            if (Vector3.Angle(lookDirection, agent.forward) <= angleDifference.value)
            {
                EndAction();
            }
        }
    }
}