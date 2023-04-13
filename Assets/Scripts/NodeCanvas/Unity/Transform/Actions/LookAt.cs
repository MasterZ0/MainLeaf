﻿using AdventureGame.Shared.NodeCanvas;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [NodeCategory(Categories.Transform)]
    [NodeDescription("Rotate axis of a GameObject")]
    public class LookAt : ActionTask<Transform> 
    {
        public bool useSpeed = true;
        public Parameter<Axis3Flags> modifiedAxis = Axis3Flags.Y;
        public Parameter<Vector3> target;

        //[ShowIf(nameof(useSpeed), 1)]
        public Parameter<float> speed;
        //[ShowIf(nameof(useSpeed), 1)]
        [Range(0, 180)]
        public Parameter<float> angleDifference = 10f;

        public override string Info => $"Look At {modifiedAxis}" + (useSpeed ? $" Speed {speed}" : string.Empty);

        protected override void StartAction()
        {
            if (!useSpeed)
            {
                Agent.rotation = GetRotation();
                EndAction();
            }
        }

        protected override void UpdateAction() 
        {
            Quaternion eulerRotation = GetRotation();
            Agent.rotation = Quaternion.Slerp(Agent.rotation, eulerRotation, speed.Value * Time.fixedDeltaTime);

            if (Vector3.Angle(eulerRotation * Vector3.forward, Agent.forward) <= angleDifference.Value)
            {
                EndAction();
            }
        }

        private Quaternion GetRotation()
        {
            Vector3 targetDirection = target.Value - Agent.position;
            Vector3 eules = Quaternion.LookRotation(targetDirection).eulerAngles;

            if (!modifiedAxis.Value.HasFlag(Axis3Flags.X))
            {
                eules.x = Agent.eulerAngles.x;
            }
            if (!modifiedAxis.Value.HasFlag(Axis3Flags.Y))
            {
                eules.y = Agent.eulerAngles.y;
            }
            if (!modifiedAxis.Value.HasFlag(Axis3Flags.Z))
            {
                eules.z = Agent.eulerAngles.z;
            }

           return Quaternion.Euler(eules);
        }
    }
}