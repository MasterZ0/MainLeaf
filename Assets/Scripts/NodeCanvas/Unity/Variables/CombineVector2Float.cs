using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using HeaderAttribute = ParadoxNotion.Design.HeaderAttribute;

namespace AdventureGame.NodeCanvas.Unity
{
    [Name("Combine Vector2 Float")]
    [Category(Categories.Variables)]
    [Description("Easy way to combine vector and floats")]
    public class CombineVector2Float : ActionTask
    {
        [Header("In")]
        public BBParameter<Vector2> initialVector;
        public BBParameter<float> xPosition;
        public BBParameter<float> yPosition;

        [Header("Config")]
        public BBParameter<bool> useFloatX;
        public BBParameter<bool> useFloatY;

        [Header("Out")]
        public BBParameter<Vector2> returnedVector;

        protected override string info
        {
            get
            {
                string initialX;
                string initialY;

                if (initialVector.isDefined)
                {
                    initialX = initialVector + ".X";
                    initialY = initialVector + ".Y";
                }
                else
                {
                    initialX = $"<b>{initialVector.value.x}</b>";
                    initialY = $"<b>{initialVector.value.y}</b>";
                }

                string x = useFloatX.value ? xPosition.ToString() : initialX;
                string y = useFloatY.value ? yPosition.ToString() : initialY;

                return $"{returnedVector} = ({x}, {y})";
            }
        }

        protected override void OnExecute()
        {
            Vector2 newVector = initialVector.value;

            if (useFloatX.value)
            {
                newVector.x = xPosition.value;
            }
            if (useFloatY.value)
            {
                newVector.y = yPosition.value;
            }

            returnedVector.value = newVector;
            EndAction(true);
        }
    }
}