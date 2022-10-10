using AdventureGame.Shared.NodeCanvas;
using AdventureGame.Shared.ExtensionMethods;
using AdventureGame.Shared.Utils;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using HeaderAttribute = ParadoxNotion.Design.HeaderAttribute;

namespace AdventureGame.NodeCanvas.Unity
{

    [Category(Categories.Variables)]
    [Description("Please describe what this ActionTask does.")]
    public class WithinOpeningAngle : ActionTask {

        [Header("Config")]
        public BBParameter<float> angleDirection;
        public BBParameter<float> openingAngle;

        [Header("Set")]
        public BBParameter<float> currentAngle;

        protected override string info => $"Filter {currentAngle} into {openingAngle}";

        protected override void OnExecute() {
            float halfAngle = openingAngle.value / 2;
            float minAngle = (angleDirection.value - halfAngle).NormalizeAngle();
            float maxAngle = (angleDirection.value + halfAngle).NormalizeAngle();

            Vector2 angleRange = new Vector2(minAngle, maxAngle);
            if (minAngle > maxAngle) {
                if (angleRange.InsideRange(currentAngle.value)) {

                    RecalculateAngle(angleRange);
                }
            }
            else {
                if (!angleRange.InsideRange(currentAngle.value)) {

                    RecalculateAngle(angleRange);
                }
            }

            EndAction(true);
        }

        private void RecalculateAngle(Vector2 range) {
            
            
            float a = MathUtils.AngleDiference(currentAngle.value, range.x);
            float b = MathUtils.AngleDiference(currentAngle.value, range.y);
            currentAngle.value = a < b ? range.x : range.y;
        }
    }
}