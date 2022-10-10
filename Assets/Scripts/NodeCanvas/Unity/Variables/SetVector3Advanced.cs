using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion;
using ParadoxNotion.Design;
using UnityEngine;
using HeaderAttribute = ParadoxNotion.Design.HeaderAttribute;

namespace AdventureGame.NodeCanvas.Unity
{
    [Name("Set Vector3 Advanced")]
    [Category(Categories.Variables)]
    [Description("Easy way to set a specific axis")]
    public class SetVector3Advanced : ActionTask 
    {
        [Header("In")]
        public BBParameter<Vector3> initialVector;
        public BBParameter<Vector3> otherVector;

        [Header("Config")]
        public OperationMethod operation = OperationMethod.Set;
        public BBParameter<bool> setX;
        public BBParameter<bool> setY;
        public BBParameter<bool> setZ;

        [Header("Out")]
        public BBParameter<Vector3> returnedVector;

        protected override string info
        {
            get
            {
                string info = string.Empty;

                if (setX.value)
                {
                    info = AddText(info, "X");
                }

                if (setY.value)
                {
                    info = AddText(info, "Y");
                }

                if (setZ.value)
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

        protected override void OnExecute()
        {
            Vector3 finalVector = initialVector.value;

            if (setX.value)
            {
                finalVector.x = OperationTools.Operate(finalVector.x, otherVector.value.x, operation);
            }
            if (setY.value)
            {
                finalVector.y = OperationTools.Operate(finalVector.y, otherVector.value.y, operation);
            }
            if (setZ.value)
            {
                finalVector.z = OperationTools.Operate(finalVector.z, otherVector.value.z, operation);
            }

            returnedVector.value = finalVector;
            EndAction(true);
        }
    }
}