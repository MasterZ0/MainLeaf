using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Variables)]
    [Description("Convert Euler Angles in a Quaterion")]
    public class EulerToQuaterion : ActionTask
    {
        public BBParameter<Vector3> euler;
        public BBParameter<Quaternion> quaterion;

        protected override string info => $"{quaterion} = Quaternion.Euler({euler})";

        protected override void OnExecute()
        {
            quaterion.value = Quaternion.Euler(euler.value);
            EndAction(true);
        }
    }
}