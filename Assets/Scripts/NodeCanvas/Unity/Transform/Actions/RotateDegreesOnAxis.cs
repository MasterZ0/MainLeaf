using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Transform)]
    [Description("Rotate axis of a GameObject")]
    public class RotateDegreesOnAxis : ActionTask<Transform>
    {
        public BBParameter<float> degrees;
        public BBParameter<float> speed;
        public BBParameter<Axis> axis;

        protected override string info => $"Rotate {agentInfo} {axis} {degrees} degrees";

        private float currentDegrees;
        private Vector3 initialAngle;

        private Func<Quaternion> updateRotation;
        protected override void OnExecute()
        {
            currentDegrees = 0;
            initialAngle = agent.eulerAngles;

            updateRotation = () => axis.value switch
            {
                Axis.X => Quaternion.Euler(currentDegrees + initialAngle.x, agent.eulerAngles.y, agent.eulerAngles.z),
                Axis.Y => Quaternion.Euler(agent.eulerAngles.x, currentDegrees + initialAngle.y, agent.eulerAngles.z),
                Axis.Z => Quaternion.Euler(agent.eulerAngles.x, agent.eulerAngles.y, currentDegrees + initialAngle.z),
                _ => throw new NotImplementedException(),
            };
        }

        protected override void OnUpdate()
        {
            currentDegrees = Mathf.MoveTowards(currentDegrees, degrees.value, Time.fixedDeltaTime * speed.value);
            agent.rotation = updateRotation();

            if (currentDegrees == degrees.value)
            {
                EndAction(true);
            }
        }
    }
}