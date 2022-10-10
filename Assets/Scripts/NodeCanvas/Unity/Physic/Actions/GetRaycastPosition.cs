using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using AdventureGame.Shared.NodeCanvas;

namespace AdventureGame.NodeCanvas.Unity
{

    [Category(Categories.Physics)]
    [Description("Raycast into a direction and return the position of the hit. Returns the position hit if possible. If nothing is hit, returnedPosition receives nullCasePosition.")]
    public class GetRaycastPosition : ActionTask<Transform>
    {
        [RequiredField] public BBParameter<Vector2> direction;
        [RequiredField] public BBParameter<Vector3> returnedPosition;
        public BBParameter<float> distance;
        public BBParameter<LayerMask> layerMask;

        protected override void OnExecute()
        {
            var hit = Physics2D.Raycast(agent.position, direction.value, distance.value, layerMask.value);
            if (hit.collider != null)
            {
                returnedPosition.value = hit.point;
                EndAction(true);
            }
            else
            {
                EndAction(false);
            }
        }
    }
}