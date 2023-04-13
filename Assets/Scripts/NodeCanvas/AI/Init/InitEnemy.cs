using AdventureGame.AI;
using AdventureGame.Data;
using AdventureGame.Shared.NodeCanvas;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;

namespace AdventureGame.NodeCanvas.AI
{
    [NodeCategory(Categories.AIInit)]
    public abstract class InitEnemy<T> : ActionTask<Enemy> where T : EnemyData
    {
        protected T EnemyData => Agent.EnemyData as T;

        protected override void StartAction()
        {
            SetParameters();

            EnemyData.OnValueChanged += OnDataChanged;

            EndAction(true);
        }

        private void OnDataChanged()
        {
            if (Agent && Agent.gameObject.activeSelf)//ownerSystemBlackboard
            {
                SetParameters();
            }
            else
            {
                EnemyData.OnValueChanged -= OnDataChanged;  
            }
        }

        protected abstract void SetParameters();
    }
}