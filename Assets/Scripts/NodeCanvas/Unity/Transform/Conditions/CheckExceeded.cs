﻿using NodeCanvas.Framework;
using AdventureGame.Shared.NodeCanvas;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{

    [Category(Categories.Transform)]
    [Description("Compare the agent passed the target + offset.")]
    public class CheckExceed : ConditionTask<Transform> 
    {
        public BBParameter<Axis3> axis;
        public BBParameter<Vector3> target;
        public BBParameter<float> offset;

        protected override string info => offset.value == 0 ?
            $"Exceeded {axis} {target}":
            $"Exceeded {axis} {target} {offset}";

        protected override bool OnCheck() 
        {
            Vector3 inverse = agent.InverseTransformPoint(target.value);
            
            return offset.value > axis.value switch
            {
                Axis3.X => inverse.x,
                Axis3.Y => inverse.y,
                Axis3.Z => inverse.z,
                _ => throw new System.NotImplementedException(),
            };
        }
    }
}