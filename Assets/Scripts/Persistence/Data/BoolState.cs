using UnityEngine;

namespace AdventureGame.Persistence
{
    public class BoolState : PersistentState<bool>
    {

        [Header("Bool State")]
        [SerializeField] private bool defaultState;
        public override bool DefaultState => defaultState;
    }
}