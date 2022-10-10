using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity {

    [Category(Categories.Rigidbody2D)]
    [Description("Create a OverlapPoint in the transform.position")]
    public class IsColliding : ConditionTask<Transform> {

        public BBParameter<Vector3> offset;
        public BBParameter<LayerMask> layerMask;

        protected override bool OnCheck() 
        {
            // TIP: If you are using grid and composite collider, remember to use Polygon option in Geometry Type
            return Physics2D.OverlapPoint(agent.position + offset.value, layerMask.value);
        }

        public override void OnDrawGizmosSelected() {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(agent.position + offset.value, .2f);
        }
    }
}