using AdventureGame.Shared;
using UnityEngine;
using Z3.UIBuilder.Core;

namespace AdventureGame.Persistence.QuestSystem
{
    /// <summary>
    /// Stores quest information.
    /// </summary>
    [CreateAssetMenu(menuName = Shared.MenuPath.ScriptableObjects +  "/QuestCounter")]
    public class QuestCounter : ScriptableObject
    {
        #region Dev Tools
        [TextArea(10, 50)]
        [SerializeField] private string questDescription = string.Empty;

        //[ShowInInspector, ReadOnly]
        private bool state;
        private bool ButtonEnable => Application.isPlaying;
        #endregion

        private string Key => $"Quest/{name}";
        
        public bool QuestIsCompleted()
        {
            state = PersistenceManager.Load(Key, false);
            return state;
        }
        
        public void Tick() => SaveState(true);
        
        private void SaveState(bool newState)
        {
            state = newState;
            PersistenceManager.Save(Key, state);
        }

        #region Dev Tools
        [Button/*, EnableIf(nameof(ButtonEnable))*/]
        private void SetCompleteQuest() => SaveState(true);

        [Button/*, EnableIf(nameof(ButtonEnable))*/]
        private void ResetQuest() => SaveState(false);
        #endregion
    }
}