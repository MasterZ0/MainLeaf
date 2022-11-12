using AdventureGame.UI.Window;
using UnityEngine;

namespace AdventureGame.Gameplay
{
    public abstract class GameController : MonoBehaviour, IControlInput
    {
        protected virtual void Awake()
        {
            GameplayReferences.SetController(this);
            GameplayReferences.SetActivePlayerInput(false, this);
            GameplayReferences.OnPlayerDeath += OnPlayerDeath;
        }

        protected abstract void OnPlayerDeath(IPlayer player);

        protected void EnableInputs() => GameplayReferences.SetActivePlayerInput(true, this);
        protected void DisableInputs() => GameplayReferences.SetActivePlayerInput(false, this);

        protected virtual void OnDestroy()
        {
            GameplayReferences.OnPlayerDeath -= OnPlayerDeath;
            GameplayReferences.Reset();
            WindowManager.CloseAllWindows();
        }
    }
}