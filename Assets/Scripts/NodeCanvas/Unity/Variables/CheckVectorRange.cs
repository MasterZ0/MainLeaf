using AdventureGame.Shared.ExtensionMethods;
using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Variables)]
    [Description("Best way to check if some object is null")]
    public class CheckVectorRange : ConditionTask
    {
        public enum RangeCompareMethod
        {
            Below,
            Inside,
            Above
        }

        public BBParameter<float> variable;
        public BBParameter<Vector2> range;
        public BBParameter<RangeCompareMethod> checkType = RangeCompareMethod.Inside;

        protected override string info
        {
            get => checkType.value switch
            {
                RangeCompareMethod.Below => $"{variable} < {range}.X",
                RangeCompareMethod.Inside => $"{variable} Inside {range}",
                RangeCompareMethod.Above => $"{variable} > {range}.Y",
                _ => throw new System.NotImplementedException(),
            };
        }

        protected override bool OnCheck()
        {
            return checkType.value switch
            {
                RangeCompareMethod.Below => variable.value < range.value.x,
                RangeCompareMethod.Inside => range.value.InsideRange(variable.value),
                RangeCompareMethod.Above => variable.value > range.value.y,
                _ => throw new System.NotImplementedException(),
            };
        }
    }
}