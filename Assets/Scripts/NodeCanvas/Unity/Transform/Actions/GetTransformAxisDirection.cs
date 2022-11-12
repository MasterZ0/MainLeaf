using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using Header = ParadoxNotion.Design.HeaderAttribute;

namespace AdventureGame.NodeCanvas.Unity.Physic
{
    [Category(Categories.Transform)]
    [Description("Get transform.axis")]
    public class GetTransformAxisDirection : ActionTask<Transform>
    {
        [Header("In")]
        public BBParameter<Direction> axisDirecition = Direction.Right;

        [Header("Out")]
        public BBParameter<Vector3> returnedValue;

        protected override string info => $"Get Transform.{axisDirecition}";

        protected override void OnExecute()
        {
            returnedValue.value = axisDirecition.value switch
            {
                Direction.Left => -agent.transform.right,
                Direction.Right => agent.transform.right,
                Direction.Up => agent.transform.up,
                Direction.Down => -agent.transform.up,
                Direction.Forward => agent.transform.forward,
                Direction.Back => -agent.transform.forward,
                _ => throw new System.NotImplementedException(),
            };

            EndAction(true);
        }
    }
}