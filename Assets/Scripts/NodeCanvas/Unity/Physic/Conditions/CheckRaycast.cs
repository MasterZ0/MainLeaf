using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using AdventureGame.Shared.NodeCanvas;
using HeaderAttribute = ParadoxNotion.Design.HeaderAttribute;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Physics)]
    [Description("Launch a Raycast and return true or false if something was hit.")]
    public class CheckRaycast : ConditionTask
    {
        [Header("Out")]
        public BBParameter<Vector3> rayOrigin;
        public BBParameter<Vector3> direction;
        public BBParameter<LayerMask> layerMask;
        public BBParameter<float> distance;
        
        [Header("Out")]
        public BBParameter<Vector2> positionHit;

        protected override bool OnCheck()
        {
            bool successful = Physics.Raycast(rayOrigin.value, direction.value, out RaycastHit hit, distance.value, layerMask.value);

            positionHit.value = hit.point;
            
            return successful;
        }
    }
}