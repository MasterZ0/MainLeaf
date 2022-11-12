using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using Header = ParadoxNotion.Design.HeaderAttribute;
using AdventureGame.Shared.NodeCanvas;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Animations)]
    [Description("Change Sprite Renderer Color Over Time")]
    public class SetColorOverTime : ActionTask<SpriteRenderer>
    {
        [Header("Input")]
        public BBParameter<float> duration;

        [Header("Output")]
        public BBParameter<Color> endColor;

        private Color startColor;
        private float timeStep;
        protected override string info => $"Change Color In Seconds: {duration}";

        protected override void OnExecute()
        {
            startColor = agent.color;
            timeStep = 0f;
        }

        protected override void OnUpdate()
        {
            if (agent.color != endColor.value && duration.value != 0f)
            {
                timeStep += Time.fixedDeltaTime / duration.value;
                agent.color = Color.Lerp(startColor, endColor.value, timeStep);
            }
            else
            {
                EndAction(true);
            }
        }
    }
}