using AdventureGame.Shared.NodeCanvas;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using UnityEngine;
using System;

namespace AdventureGame.NodeCanvas.Unity
{
    //[Name("Set Vector2 Advanced")]
    [NodeCategory(Categories.Variables)]
    [NodeDescription("Easy way to set a specific axis")]
    public class SetVector2Advanced : ActionTask
    {
        [Header("In")]
        public Parameter<Vector2> initialVector;
        public Parameter<Vector2> otherVector;

        [Header("Config")]
        public OperationMethod operation = OperationMethod.Set;
        public Parameter<bool> setX;
        public Parameter<bool> setY;

        [Header("Out")]
        public Parameter<Vector2> returnedVector;

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
                finalVector.x = OperationTools.Operate(finalVector.x, otherVector.Value.x, operation);
            }
            if (setY.Value)
            {
                finalVector.y = OperationTools.Operate(finalVector.y, otherVector.Value.y, operation);
            }

            returnedVector.Value = finalVector;
            EndAction(true);
        }
    }
}