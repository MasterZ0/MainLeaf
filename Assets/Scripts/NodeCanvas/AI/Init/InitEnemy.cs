using AdventureGame.AI;
using AdventureGame.Data;
using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace AdventureGame.NodeCanvas.AI
{
    [Category(Categories.AIInit)]
    public abstract class InitEnemy<T> : ActionTask<Enemy> where T : EnemyData
    {
        protected T EnemyData => agent.EnemyData as T;

        protected override void OnExecute()
        {
            SetParameters();

            EnemyData.OnValueChanged += OnDataChanged;

            EndAction(true);
        }

        private void OnDataChanged()
        {
            if (agent && agent.gameObject.activeSelf)//ownerSystemBlackboard
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