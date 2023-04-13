using AdventureGame.AI;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using AdventureGame.Shared.NodeCanvas;

namespace AdventureGame.NodeCanvas.AI 
{
    [NodeCategory(Categories.AI)]
    [NodeDescription("Kills given enemy")]
    public class FinishEnemyDeath : ActionTask<Enemy> 
    {
        protected override void StartAction() 
        {
            Agent.FinishEnemyDeath();
            EndAction(true);
        }
    }
}