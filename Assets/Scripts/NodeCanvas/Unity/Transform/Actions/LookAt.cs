using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Transform)]
    [Description("Rotate axis of a GameObject")]
    public class LookAt : ActionTask<Transform> 
    {
        public bool useSpeed = true;
        public BBParameter<Axis3Flags> modifiedAxis = Axis3Flags.Y;
        public BBParameter<Vector3> target;

        [ShowIf(nameof(useSpeed), 1)]
        public BBParameter<float> speed;
        [ShowIf(nameof(useSpeed), 1)]
        [SliderField(0, 180)]
        public BBParameter<float> angleDifference = 10f;

        protected override string info => $"Look At {modifiedAxis}" + (useSpeed ? $" Speed {speed}" : string.Empty);

        protected override void OnExecute()
        {
            if (!useSpeed)
            {
                agent.rotation = GetRotation();
                EndAction();
            }
        }

        protected override void OnUpdate() 
        {
            Quaternion eulerRotation = GetRotation();
            agent.rotation = Quaternion.Slerp(agent.rotation, eulerRotation, speed.value * Time.fixedDeltaTime);

            if (Vector3.Angle(eulerRotation * Vector3.forward, agent.forward) <= angleDifference.value)
            {
                EndAction();
            }
        }

        private Quaternion GetRotation()
        {
            Vector3 targetDirection = target.value - agent.position;
            Vector3 eules = Quaternion.LookRotation(targetDirection).eulerAngles;

            if (!modifiedAxis.value.HasFlag(Axis3Flags.X))
            {
                eules.x = agent.eulerAngles.x;
            }
            if (!modifiedAxis.value.HasFlag(Axis3Flags.Y))
            {
                eules.y = agent.eulerAngles.y;
            }
            if (!modifiedAxis.value.HasFlag(Axis3Flags.Z))
            {
                eules.z = agent.eulerAngles.z;
            }

           return Quaternion.Euler(eules);
        }
    }
}