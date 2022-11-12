using UnityEngine;

namespace AdventureGame.Persistence
{
    public interface IPersistentState
    {
        void SaveState();
        void LoadState();
    }

    /// <summary>
    /// Automation to save data using a MonoBehaviour
    /// </summary>
    /// <typeparam name="T">Serialized data that will be saved</typeparam>
    public abstract class PersistentState<T> : MonoBehaviour, IPersistentState
    {
        [Tooltip("Save when destroy the GameObject")]
        [SerializeField] private bool saveOnDestroy;

        public T CurrentState { get; set; }
        public abstract T DefaultState { get; }
        
        private string keyName;

        private void Start()
        {
            Transform biggestParent = transform.root;
            keyName = PersistenceManager.GetTypeKey(biggestParent.name, gameObject);
            LoadState();
        }

        private void OnDestroy()
        {
            if (saveOnDestroy)
                SaveState();
        }

        public virtual void LoadState()
        {
            CurrentState = PersistenceManager.Load(keyName, DefaultState);
        }

        public virtual void SaveState()
        {
            PersistenceManager.Save(keyName, CurrentState);
        }
    }
}