using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using HeaderAttribute = ParadoxNotion.Design.HeaderAttribute;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Animations)]
    [Description("Play animation by state name")]
    public class UpdateAnimationMotionParameter : ActionTask<Animator>
    {
        [Header("Variables")]
        public BBParameter<Vector3> currentVelocity;
        public BBParameter<float> maxVelocityScale;
        public BBParameter<float> animationBlendDamp;

        [Header("Parameters")]
        public BBParameter<string> velocityMagnitude = "MoveSpeed";
        public BBParameter<string> velocityX = "VelocityX";
        public BBParameter<string> velocityY = "VelocityY";
        public BBParameter<string> velocityZ = "VelocityZ";

        protected override void OnExecute()
        {
            float maxScale = maxVelocityScale.value == 0 ? 1 : maxVelocityScale.value; // Avoid division by 0
            Vector3 velocityScale = currentVelocity.value / maxScale;

            SetFloat(velocityMagnitude.value, velocityScale.magnitude);
            SetFloat(velocityX.value, velocityScale.x);
            SetFloat(velocityY.value, velocityScale.y);
            SetFloat(velocityZ.value, velocityScale.z);

            EndAction();
        }

        private void SetFloat(string parameter, float value) => agent.SetFloat(parameter, value, animationBlendDamp.value, Time.fixedDeltaTime);
    }
}