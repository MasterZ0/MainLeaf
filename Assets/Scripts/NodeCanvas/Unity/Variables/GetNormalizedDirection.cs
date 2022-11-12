using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using AdventureGame.Shared.NodeCanvas;
using HeaderAttribute = ParadoxNotion.Design.HeaderAttribute;

namespace AdventureGame.NodeCanvas.Unity.Physic
{
    [Category(Categories.Variables)]
    [Description("Get (target.value - from.value).normalized")]
    public class GetNormalizedDirection : ActionTask
    {
        [Header("In")]
        public BBParameter<Vector3> from;
        public BBParameter<Vector3> target;

        [Header("Out")]
        public BBParameter<Vector3> direction;

        protected override string info => $"Get Direction {from} to {target}";
        protected override void OnExecute()
        {
            direction.value = (target.value - from.value).normalized;
            EndAction(true);
        }
    }
}