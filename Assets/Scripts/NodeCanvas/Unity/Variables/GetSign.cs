using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Variables)]
    [Description("Gets the sign of a number (-1 if negative and 1 if positive)")]
    public class GetSign : ActionTask
    {
        public BBParameter<float> inNumber;
        public BBParameter<float> outSign;

        protected override string info => $"{outSign.name} = Sign of {inNumber.name}";

        protected override void OnExecute()
        {
            outSign.value = Mathf.Sign(inNumber.value);
            EndAction(true);
        }
    }
}