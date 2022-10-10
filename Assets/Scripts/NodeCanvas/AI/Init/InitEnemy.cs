using AdventureGame.AI;
using AdventureGame.Data;
using NodeCanvas.Framework;

namespace AdventureGame.NodeCanvas.AI {

    public abstract class InitEnemy : ActionTask<Enemy>
    {
        protected override void OnExecute()
        {
            EnemyData data = Init();
            agent.Setup(data);
            EndAction(true);
        }

        protected abstract EnemyData Init();
    }
}