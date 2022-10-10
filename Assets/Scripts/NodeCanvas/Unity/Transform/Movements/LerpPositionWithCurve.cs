using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using AdventureGame.Shared.NodeCanvas;

namespace AdventureGame.NodeCanvas.Unity.Movement
{

    [Category(Categories.Movement)]
    [Description("Move a GameObject to the target position, through an animationCurve.")]
    public class LerpPositionWithCurve : ActionTask<Transform>
    {
        [RequiredField] public BBParameter<Vector3> targetPosition;
        [RequiredField] public BBParameter<float> time;
        public BBParameter<AnimationCurve> animationCurve;

        private Vector2 initPosition;
        private Vector2 finalPosition;
        private float t;

        protected override string info => $"Lerping To {targetPosition}";

        protected override void OnExecute()
        {
            initPosition = agent.position;
            finalPosition = targetPosition.value;
            t = 0f;
        }

        protected override void OnUpdate()
        {
            t += Time.deltaTime / time.value;
            agent.position = Vector2.Lerp(initPosition, finalPosition, animationCurve.value.Evaluate(t));

            if (t > 1)
                EndAction(true);
        }
    }
}