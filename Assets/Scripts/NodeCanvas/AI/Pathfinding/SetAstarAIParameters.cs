using AdventureGame.Data;
using AdventureGame.Shared.NodeCanvas;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using Pathfinding;

namespace AdventureGame.NodeCanvas.AI.Pathfinding
{
    //[Name("Set AstarAI Parameters")]
    [NodeCategory(Categories.Pathfinding)]
    [NodeDescription("Set the parameters defined in IAstarAI")]
    public class SetAstarAIParameters : ActionTask<AIPath>
    {
        public Parameter<AIPathParameters> aiPathParameters;

        public override string Info => $"AstarAI Parameters = {aiPathParameters}";

        protected override void StartAction()
        {
            Agent.maxSpeed = aiPathParameters.Value.maxSpeed;
            Agent.rotationSpeed = aiPathParameters.Value.rotationSpeed;
            Agent.slowdownDistance = aiPathParameters.Value.slowdownDistance;
            Agent.endReachedDistance = aiPathParameters.Value.endReachedDistance;

            EndAction();
        }
    }
}