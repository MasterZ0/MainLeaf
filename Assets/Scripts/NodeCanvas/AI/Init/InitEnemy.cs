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
        public abstract T EnemyData { get; }

        protected override void OnExecute()
        {
            SetParameters();
            agent.Setup(EnemyData);

            EnemyData.OnValueChanged += OnDataChanged;

            EndAction(true);
        }

        protected override void OnStop()
        {
            EnemyData.OnValueChanged -= OnDataChanged;
        }

        private void OnDataChanged()
        {
            if (ownerSystemBlackboard != null)
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