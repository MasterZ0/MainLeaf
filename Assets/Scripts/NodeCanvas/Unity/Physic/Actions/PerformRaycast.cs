using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Physics)]
    [Description("<<REVIEW>>Performs a raycast with the given parameters and returns all hit results")]
    public class PerformRaycast : ActionTask
    {
        [ParadoxNotion.Design.Header("Parameters")]
        public BBParameter<Vector3> origin;
        public BBParameter<Vector3> direction;
        public BBParameter<LayerMask> mask;
        public BBParameter<float> distance = float.MaxValue;
        
        [ParadoxNotion.Design.Header("Return Conditions")]
        public BBParameter<bool> useHitAsReturn;
        public BBParameter<bool> reverseReturn;

        [ParadoxNotion.Design.Header("Hit Results")]
        public BBParameter<bool> raycastHit;
        public BBParameter<float> hitDistance;
        public BBParameter<Vector3> point;
        public BBParameter<Vector3> normal;
        public BBParameter<Collider2D> otherCollider;
        public BBParameter<Transform> otherTransform;

        protected override string info => $"Raycast {origin.name} -> {direction.value}\n" +
                                          $"(Returns {(useHitAsReturn.value ? reverseReturn.value ? "!Hit" : "Hit" : reverseReturn.value ? "Failure" : "Success")})";

        protected override void OnExecute()
        {
            RaycastHit2D hit = Physics2D.Raycast(origin.value, direction.value, distance.value, mask.value);
            
            raycastHit.value = hit;
            point.value = hit.point;
            normal.value = hit.normal;
            otherCollider.value = hit.collider;
            otherTransform.value = hit.transform;
            hitDistance.value = hit.distance;

            EndAction(useHitAsReturn.value ? reverseReturn.value ? !hit : hit : !reverseReturn.value);
        }
    }
}