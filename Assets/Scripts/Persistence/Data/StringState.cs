using UnityEngine;

namespace AdventureGame.Persistence
{
    public class StringState : PersistentState<string> {

        [Header("String State")]
        [SerializeField] private string defaultState;
        public override string DefaultState => defaultState;
    }
}