using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion;
using ParadoxNotion.Design;
using UnityEngine;
using HeaderAttribute = ParadoxNotion.Design.HeaderAttribute;

namespace AdventureGame.NodeCanvas.Unity
{
    [Name("Set Vector2 Advanced Float")]
    [Category(Categories.Variables)]
    [Description("Easy way to set a specific axis")]
    public class SetVector2AdvancedFloat : ActionTask
    {
        [Header("In")]
        public BBParameter<Vector2> initialVector;
        public BBParameter<float> valueX;
        public BBParameter<float> valueY;

        [Header("Config")]
        public OperationMethod operation = OperationMethod.Set;
        public BBParameter<bool> setX;
        public BBParameter<bool> setY;

        [Header("Out")]
        public BBParameter<Vector2> returnedVector;

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
            Vector2 finalVector = initialVector.value;

            if (setX.value)
            {
                finalVector.x = OperationTools.Operate(finalVector.x, valueX.value, operation);
            }
            if (setY.value)
            {
                finalVector.y = OperationTools.Operate(finalVector.y, valueY.value, operation);
            }

            returnedVector.value = finalVector;
            EndAction(true);
        }
    }
}