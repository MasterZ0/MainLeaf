using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace AdventureGame.NodeCanvas.Unity.NodeCanvas.Unity.Variables
{
    [Category(Categories.Variables)]
    [Description("Performs boolean calculations")]
    public class SetBooleanAdvanced : ActionTask
    {
        [Header("Input")]
        public BBParameter<bool> booleanA;
        public BBParameter<bool> booleanB;
        public BooleanOperations operation;
        
        [Header("Output")]
        public BBParameter<bool> output;

        protected override string info => GetOperatorInfo();
        
        protected override void OnExecute()
        {
            output.value = operation switch
            {
                BooleanOperations.Or => booleanA.value || booleanB.value,
                BooleanOperations.And => booleanA.value && booleanB.value,
                BooleanOperations.Not => !booleanA.value,
                BooleanOperations.Xor => booleanA.value != booleanB.value,
                BooleanOperations.Xand => booleanA.value == booleanB.value,
                BooleanOperations.Nor => !(booleanA.value || booleanB.value),
                BooleanOperations.Nand => !(booleanA.value && booleanB.value),
                _ => false
            };
            
            EndAction(true);
        }
        
        private string GetOperatorInfo()
        {
            return operation switch
            {
                BooleanOperations.Or => $"{output.name} = {booleanA.name} OR {booleanB.name}",
                BooleanOperations.And => $"{output.name} = {booleanA.name} AND {booleanB.name}",
                BooleanOperations.Not => $"{output.name} = !{booleanA.name}",
                BooleanOperations.Xor => $"{output.name} = {booleanA.name} != {booleanB.name}",
                BooleanOperations.Xand => $"{output.name} = {booleanA.name} == {booleanB.name}",
                BooleanOperations.Nor => $"{output.name} = !({booleanA.name} OR {booleanB.name})",
                BooleanOperations.Nand => $"{output.name} = !({booleanA.name} AND {booleanB.name})",
                _ => "NULL"
            };
        }

        public enum BooleanOperations
        {
            Or = 1,
            And = 2,
            Not = 3,
            Xor = 4,
            Xand = 5,
            Nor = 6,
            Nand = 7
        }
    }
}