using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity {

    [Category(Categories.Physics)]
    [Description("Create a OverlapPoint in the transform.position")]
    public class OverlapPoint : ConditionTask<Transform> 
    {
        public BBParameter<Vector3> offset;
        public BBParameter<LayerMask> layerMask;

        protected override bool OnCheck() 
        {
            return Physics.CheckSphere(agent.position + offset.value, float.MinValue, layerMask.value);
        }

        public override void OnDrawGizmosSelected() {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(agent.position + offset.value, .2f);
        }
    }
}