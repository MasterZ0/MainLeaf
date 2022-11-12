using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using HeaderAttribute = ParadoxNotion.Design.HeaderAttribute;

namespace AdventureGame.NodeCanvas.Unity
{
    [Name("Combine Vector3 Float")]
    [Category(Categories.Variables)]
    [Description("Easy way to combine vector and floats")]
    public class CombineVector3Float : ActionTask 
    {
        [Header("In")]
        public BBParameter<Vector3> initialVector;
        public BBParameter<float> xPosition;
        public BBParameter<float> yPosition;
        public BBParameter<float> zPosition;

        [Header("Config")]
        public BBParameter<bool> useFloatX;
        public BBParameter<bool> useFloatY;
        public BBParameter<bool> useFloatZ;

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

                string x = useFloatX.value ? xPosition.ToString() : initialX;
                string y = useFloatY.value ? yPosition.ToString() : initialY;
                string z = useFloatZ.value ? zPosition.ToString() : initialZ;

                return $"{returnedVector} = ({x}, {y}, {z})";
            } 
        }

        protected override void OnExecute() {
            Vector3 newVector = initialVector.value;

            if (useFloatX.value)
            {
                newVector.x = xPosition.value;
            }
            if (useFloatY.value)
            {
                newVector.y = yPosition.value;
            }
            if (useFloatZ.value)
            {
                newVector.z = zPosition.value;
            }

            returnedVector.value = newVector;
            EndAction(true);
        }
    }
}