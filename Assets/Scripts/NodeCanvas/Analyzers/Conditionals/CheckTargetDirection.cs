using NodeCanvas.Framework;
using ParadoxNotion.Design;
using AdventureGame.Shared.NodeCanvas;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Analyzers {

    [Category(Categories.Analyzers)]
    [Description("Compare the agent position with the target position")]
    public class CheckTargetDirection : ConditionTask<Transform> {

        public BBParameter<Vector3> target;
        public BBParameter<Direction> direction;

        protected override string info => $"Targer Direction == {direction}";

        protected override bool OnCheck() 
        {
            return direction.value switch
            {
                Direction.Left => target.value.x < agent.position.x,
                Direction.Right => target.value.x > agent.position.x,
                Direction.Up => target.value.y > agent.position.y,
                Direction.Down => target.value.y < agent.position.y,
                Direction.Forward => target.value.z < agent.position.z,
                Direction.Back => target.value.z > agent.position.z,
                _ => throw new System.NotImplementedException(),
            };
        }
    }
}