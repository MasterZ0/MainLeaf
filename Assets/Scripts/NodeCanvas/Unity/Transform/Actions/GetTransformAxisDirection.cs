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
        public BBParameter<AxisDirection> axisDirecition = AxisDirection.Right;

        [Header("Out")]
        public BBParameter<Vector3> returnedValue;

        protected override string info => $"Get Transform.{axisDirecition}";

        protected override void OnExecute()
        {
            returnedValue.value = axisDirecition.value switch
            {
                AxisDirection.Left => -agent.transform.right,
                AxisDirection.Right => agent.transform.right,
                AxisDirection.Up => agent.transform.up,
                AxisDirection.Down => -agent.transform.up,
                AxisDirection.Forward => agent.transform.forward,
                AxisDirection.Back => -agent.transform.forward,
                _ => throw new System.NotImplementedException(),
            };

            EndAction(true);
        }
    }
}