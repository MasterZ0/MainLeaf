using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using HeaderAttribute = ParadoxNotion.Design.HeaderAttribute;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Variables)]
    [Description("Easy way to combine vectors")]
    public class CombineVector2 : ActionTask
    {
        [Header("In")]
        public BBParameter<Vector2> initialVector;
        public BBParameter<Vector2> otherVector;

        [Header("Config")]
        public BBParameter<bool> useOtherX;
        public BBParameter<bool> useOtherY;

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

                string otherX;
                string otherY;

                if (otherVector.isDefined)
                {
                    otherX = otherVector + ".X";
                    otherY = otherVector + ".Y";
                }
                else
                {
                    otherX = $"<b>{otherVector.value.x}</b>";
                    otherY = $"<b>{otherVector.value.y}</b>";
                }

                string x = useOtherX.value ? otherX : initialX;
                string y = useOtherY.value ? otherY : initialY;

                return $"{returnedVector} = ({x}, {y})";
            }
        }

        protected override void OnExecute()
        {
            Vector2 finalVector;

            finalVector.x = useOtherX.value ? otherVector.value.x : initialVector.value.x;
            finalVector.y = useOtherY.value ? otherVector.value.y : initialVector.value.y;

            returnedVector.value = finalVector;

            EndAction(true);
        }
    }
}