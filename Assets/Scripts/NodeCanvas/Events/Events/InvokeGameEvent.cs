using AdventureGame.Shared.NodeCanvas;
using AdventureGame.Shared;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;

namespace AdventureGame.NodeCanvas.Events
{
    [NodeCategory(Categories.Events)]
    [NodeDescription("Invoke a Game Event")]
    public class InvokeGameEvent : ActionTask 
    {

        /*[RequiredField]*/ public Parameter<GameEvent> gameEvent;

        public override string Info => !gameEvent.isNoneOrNull ?
            $"Raise Event <b>{gameEvent.Value.name}</b>" : name; 

        protected override void StartAction() {
            gameEvent.Value.Invoke();
            EndAction(true);
        }
    }
}