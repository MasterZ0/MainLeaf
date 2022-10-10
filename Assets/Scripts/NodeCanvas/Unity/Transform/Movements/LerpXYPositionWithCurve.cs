using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using AdventureGame.Shared.NodeCanvas;
namespace AdventureGame.NodeCanvas.Unity.Movement
{
    [Category(Categories.Movement)]
    [Description("Move a GameObject to the target position, through an animationCurve.")]
    public class LerpXYPositionWithCurve : ActionTask<Transform>
    {
        public BBParameter<Vector3> targetPosition;
        public BBParameter<float> duration;
        public BBParameter<AnimationCurve> curveX;
        public BBParameter<AnimationCurve> curveY;

        private Vector2 initPosition;
        private Vector2 finalPosition;
        private Vector2 currentPosition;
        private float t;

        protected override string info => $"Lerping X/Y To {targetPosition}";

        protected override void OnExecute()
        {
            initPosition = agent.position;
            finalPosition = targetPosition.value;
            currentPosition = initPosition;
            t = 0f;
        }

        protected override void OnUpdate()
        {
            t += Time.fixedDeltaTime / duration.value;
            currentPosition.x = Mathf.Lerp(initPosition.x, finalPosition.x, curveX.value.Evaluate(t));
            currentPosition.y = Mathf.Lerp(initPosition.y, finalPosition.y, curveY.value.Evaluate(t));
            agent.position = currentPosition;

            if (t >= 1)
                EndAction(true);
        }
    }
}