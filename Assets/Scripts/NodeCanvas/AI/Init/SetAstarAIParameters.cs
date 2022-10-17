using AdventureGame.Data;
using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Pathfinding;

namespace AdventureGame.NodeCanvas.AI
{
    [Name("Set AstarAI Parameters")]
    [Category(Categories.AIPath)]
    [Description("Set the parameters defined in IAstarAI")]
    public class SetAstarAIParameters : ActionTask<AIPath>
    {
        public BBParameter<AIPathParameters> aiPathParameters;
        protected override void OnExecute()
        {
            agent.maxSpeed = aiPathParameters.value.maxSpeed;
            agent.rotationSpeed = aiPathParameters.value.rotationSpeed;
            agent.slowdownDistance = aiPathParameters.value.slowdownDistance;
            agent.endReachedDistance = aiPathParameters.value.endReachedDistance;

            EndAction();
        }
    }
}