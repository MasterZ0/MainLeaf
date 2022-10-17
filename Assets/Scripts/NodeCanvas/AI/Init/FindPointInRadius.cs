using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Pathfinding;
using UnityEngine;
using HeaderAttribute = ParadoxNotion.Design.HeaderAttribute;

namespace AdventureGame.NodeCanvas.AI
{
    [Category(Categories.AIPath)]
    [Description("Set the parameters defined in IAstarAI")]
    public class RandomPointInCircle : ActionTask<AIPath>
    {
        [Header("In")]
        //public BBParameter<Axis> axis = Axis.Y;
        public BBParameter<Vector3> center;
        public BBParameter<float> radius;

        [Header("Out")]
        public BBParameter<Vector3> targetPoint;

        protected override void OnExecute()
        {
            int angle = Random.Range(0, 360);
            float area = Random.Range(0, radius.value);
            Vector3 offset = new Vector3()
            {
                x = area * Mathf.Cos(angle * Mathf.Deg2Rad),
                y = 0f,
                z = area * Mathf.Sin(angle * Mathf.Deg2Rad)
            };

            targetPoint.value = center.value + offset;
            EndAction();
        }
    }
}