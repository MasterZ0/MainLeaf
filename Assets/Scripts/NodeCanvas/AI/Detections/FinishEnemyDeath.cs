using AdventureGame.AI;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using AdventureGame.Shared.NodeCanvas;

namespace AdventureGame.NodeCanvas.AI 
{
    [Category(Categories.AI)]
    [Description("Kills given enemy")]
    public class FinishEnemyDeath : ActionTask<Enemy> 
    {
        protected override void OnExecute() 
        {
            agent.FinishEnemyDeath();
            EndAction(true);
        }
    }
}