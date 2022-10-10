using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using HeaderAttribute = ParadoxNotion.Design.HeaderAttribute;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Variables)]
    [Description("Easy way to combine vectors")]
    public class CombineVector3 : ActionTask
    {
        [Header("In")]
        public BBParameter<Vector3> initialVector;
        public BBParameter<Vector3> otherVector;

        [Header("Config")]
        public BBParameter<bool> useOtherX;
        public BBParameter<bool> useOtherY;
        public BBParameter<bool> useOtherZ;

        [Header("Out")]
        public BBParameter<Vector3> returnedVector;

        protected override string info
        {
            get
            {
                string initialX;
                string initialY;
                string initialZ;

                if (initialVector.isDefined)
                {
                    initialX = initialVector + ".X";
                    initialY = initialVector + ".Y";
                    initialZ = initialVector + ".Z";
                }
                else
                {
                    initialX = $"<b>{initialVector.value.x}</b>";
                    initialY = $"<b>{initialVector.value.y}</b>";
                    initialZ = $"<b>{initialVector.value.z}</b>";
                }

                string otherX;
                string otherY;
                string otherZ;

                if (otherVector.isDefined)
                {
                    otherX = otherVector + ".X";
                    otherY = otherVector + ".Y";
                    otherZ = otherVector + ".Z";
                }
                else
                {
                    otherX = $"<b>{otherVector.value.x}</b>";
                    otherY = $"<b>{otherVector.value.y}</b>";
                    otherZ = $"<b>{otherVector.value.z}</b>";
                }

                string x = useOtherX.value ? otherX : initialX;
                string y = useOtherY.value ? otherY : initialY;
                string z = useOtherZ.value ? otherZ : initialZ;

                return $"{returnedVector} = ({x}, {y}, {z})";
            }
        }

        protected override void OnExecute()
        {
            Vector3 finalVector;

            finalVector.x = useOtherX.value ? otherVector.value.x : initialVector.value.x;
            finalVector.y = useOtherY.value ? otherVector.value.y : initialVector.value.y;
            finalVector.z = useOtherZ.value ? otherVector.value.z : initialVector.value.z;

            returnedVector.value = finalVector;

            EndAction(true);
        }
    }
}