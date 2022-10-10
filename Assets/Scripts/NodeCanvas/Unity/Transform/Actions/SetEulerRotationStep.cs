using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Transform)]
    [Description("Set Transform.position")]
    public class SetEulerZRotationStep : ActionTask<Transform>
    {
        public BBParameter<float> rotationZ;
        public BBParameter<float> step;

        private float previousYRotation;

        protected override string info => $"Euler Z Rotation = {rotationZ}, Step = {step}";

        protected override void OnUpdate()
        {
            float rotationZValue = 0;

            float yRotation = Mathf.Round(agent.transform.eulerAngles.y);
            bool inverted = yRotation != previousYRotation;
            previousYRotation = yRotation;

            if (yRotation > 0)
                rotationZValue = -rotationZ.value;
            else
                rotationZValue = rotationZ.value;

            if (inverted)
            {
                agent.transform.rotation = Quaternion.Euler(agent.transform.rotation.eulerAngles.x, agent.transform.rotation.eulerAngles.y, rotationZValue);
            }
            else
            {
                Quaternion currentRotation = agent.transform.rotation;
                Quaternion wantedRotation = Quaternion.Euler(agent.transform.rotation.eulerAngles.x, agent.transform.rotation.eulerAngles.y, rotationZValue);
                agent.transform.rotation = Quaternion.RotateTowards(currentRotation, wantedRotation, step.value);
            }


            if (agent.transform.rotation.z == rotationZValue)
            {
                EndAction(true);
            }
        }
    }
}