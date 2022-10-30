using AdventureGame.UI.Window;
using UnityEngine;

namespace AdventureGame.Gameplay
{
    public abstract class GameController : MonoBehaviour, IControlInput
    {
        protected virtual void Awake()
        {
            GameplayReferences.SetReferences(this);
            GameplayReferences.SetActivePlayerInput(false, this);
        }

        protected void EnableInputs() => GameplayReferences.SetActivePlayerInput(true, this);
        protected void DisableInputs() => GameplayReferences.SetActivePlayerInput(false, this);

        protected virtual void OnDestroy()
        {
            GameplayReferences.Reset();
            WindowManager.CloseAllWindows();
        }
    }
}