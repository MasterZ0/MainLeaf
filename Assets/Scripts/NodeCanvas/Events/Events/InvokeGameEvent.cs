using AdventureGame.Shared.NodeCanvas;
using AdventureGame.Shared;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace AdventureGame.NodeCanvas.Events
{
    [Category(Categories.Events)]
    [Description("Invoke a Game Event")]
    public class InvokeGameEvent : ActionTask 
    {

        [RequiredField] public BBParameter<GameEvent> gameEvent;

        protected override string info => !gameEvent.isNoneOrNull ?
            $"Raise Event <b>{gameEvent.value.name}</b>" : name; 

        protected override void OnExecute() {
            gameEvent.value.Invoke();
            EndAction(true);
        }
    }
}