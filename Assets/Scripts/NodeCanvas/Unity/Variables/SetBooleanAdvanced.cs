using AdventureGame.Shared.NodeCanvas;
using UnityEngine;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;

namespace AdventureGame.NodeCanvas.Unity.NodeCanvas.Unity.Variables
{
    [NodeCategory(Categories.Variables)]
    [NodeDescription("Performs boolean calculations")]
    public class SetBooleanAdvanced : ActionTask
    {
        [Header("Input")]
        public Parameter<bool> booleanA;
        public Parameter<bool> booleanB;
        public BooleanOperations operation;
        
        [Header("Output")]
        public Parameter<bool> output;

        public override string Info => GetOperatorInfo();
        
        protected override void StartAction()
        {
            output.Value = operation switch
            {
                BooleanOperations.Or => booleanA.Value || booleanB.Value,
                BooleanOperations.And => booleanA.Value && booleanB.Value,
                BooleanOperations.Not => !booleanA.Value,
                BooleanOperations.Xor => booleanA.Value != booleanB.Value,
                BooleanOperations.Xand => booleanA.Value == booleanB.Value,
                BooleanOperations.Nor => !(booleanA.Value || booleanB.Value),
                BooleanOperations.Nand => !(booleanA.Value && booleanB.Value),
                _ => false
            };
            
            EndAction(true);
        }
        
        private string GetOperatorInfo()
        {
            return operation switch
            {
                //BooleanOperations.Or => $"{output.name} = {booleanA.name} OR {booleanB.name}",
                //BooleanOperations.And => $"{output.name} = {booleanA.name} AND {booleanB.name}",
                //BooleanOperations.Not => $"{output.name} = !{booleanA.name}",
                //BooleanOperations.Xor => $"{output.name} = {booleanA.name} != {booleanB.name}",
                //BooleanOperations.Xand => $"{output.name} = {booleanA.name} == {booleanB.name}",
                //BooleanOperations.Nor => $"{output.name} = !({booleanA.name} OR {booleanB.name})",
                //BooleanOperations.Nand => $"{output.name} = !({booleanA.name} AND {booleanB.name})",
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