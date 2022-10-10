using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Analyzers {

    [Category(Categories.Analyzers)]
    [Description("Compare the euler angles")]
    public class CheckRotation : ConditionTask<Transform> {

        public BBParameter<Vector3> rotation;

        private const float checkThreshold = 0.02f;

        protected override string info => $"Rotation == {rotation}";

        protected override bool OnCheck()
        {
            if (Mathf.Abs(agent.eulerAngles.x - rotation.value.x) < checkThreshold)
                if (Mathf.Abs(agent.eulerAngles.y - rotation.value.y) < checkThreshold)
                    if (Mathf.Abs(agent.eulerAngles.z - rotation.value.z) < checkThreshold)
                        return true;            
            return false;
        }
    }
}