﻿using AdventureGame.Shared.NodeCanvas;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    //[Name("Set Vector3 Advanced Float")]
    [NodeCategory(Categories.Variables)]
    [NodeDescription("Easy way to set a specific axis")]
    public class SetVector3AdvancedFloat : ActionTask
    {
        [Header("In")]
        public Parameter<Vector3> initialVector;
        public Parameter<float> valueX;
        public Parameter<float> valueY;
        public Parameter<float> valueZ;

        [Header("Config")]
        public OperationMethod operation = OperationMethod.Set;
        public Parameter<bool> setX;
        public Parameter<bool> setY;
        public Parameter<bool> setZ;

        [Header("Out")]
        public Parameter<Vector3> returnedVector;

        public override string Info
        {
            get
            {
                string info = string.Empty;

                if (setX.Value)
                {
                    info = AddText(info, "X");
                }

                if (setY.Value)
                {
                    info = AddText(info, "Y");
                }

                if (setZ.Value)
                {
                    info = AddText(info, "Z");
                }

                if (string.IsNullOrEmpty(info))
                {
                    return name;
                }

                return info;
            }
        }

        private string AddText(string info, string axis)
        {
            axis = $"<b>{axis}</b>";
            if (string.IsNullOrEmpty(info))
            {
                return $"{returnedVector} {operation} {axis}";
            }

            return info + $", {axis}";
        }

        protected override void StartAction()
        {
            Vector3 finalVector = initialVector.Value;

            if (setX.Value)
            {
                finalVector.x = OperationTools.Operate(finalVector.x, valueX.Value, operation);
            }
            if (setY.Value)
            {
                finalVector.y = OperationTools.Operate(finalVector.y, valueY.Value, operation);
            }
            if (setZ.Value)
            {
                finalVector.z = OperationTools.Operate(finalVector.z, valueZ.Value, operation);
            }

            returnedVector.Value = finalVector;
            EndAction(true);
        }
    }
}