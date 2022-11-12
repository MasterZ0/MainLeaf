using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using static AdventureGame.NodeCanvas.Utils.GraphReferences;

namespace AdventureGame.NodeCanvas.Utils 
{ 
    [Category(Categories.Events)]
    [Description("Sends an event to a graph")]
    public class SendEventToReference : ActionTask<GraphReferences> 
    {
        [RequiredField] public BBParameter<string> graph;
        [RequiredField] public BBParameter<string> eventName;

        protected override string info => $"Send Event to {graph} [{eventName}]";

        protected override void OnExecute() 
        {
            GraphOwner owner = agent.GetGraph(graph.value);
            owner.SendEvent(eventName.value);
            EndAction(true);
        }
    }
    
    [Category(Categories.Events)]
    [Description("Sends an event to a graph passing a value")]
    public class SendEventToReference<T> : ActionTask<GraphReferences> 
    {
        [RequiredField] public BBParameter<string> graph;
        [RequiredField] public BBParameter<string> eventName;
        public BBParameter<T> value;

        protected override string info => $"Send Event to {graph} [{eventName}]\nPassing {value}";

        protected override void OnExecute() 
        {
            GraphOwner owner = agent.GetGraph(graph.value);
            owner.SendEvent(eventName.value, value.value, agent.transform);
            EndAction(true);
        }
    }
    
    [Category(Categories.Events)]
    [Description("Sends an event to all graphs at Graph References")]
    public class SendEventToReferences : ActionTask<GraphReferences>
    {
        [RequiredField] public BBParameter<string> eventName;
        
        protected override string info => $"Send {eventName.value} to all graphs";

        protected override void OnExecute() 
        {
            GraphReference[] owners = agent.Graphs;
            
            foreach (GraphReference owner in owners)
                owner.graphOwner.SendEvent(eventName.value);
            
            EndAction(true);
        }
    }
    
    [Category(Categories.Events)]
    [Description("Sends an event to all graphs at Graph References")]
    public class SendEventToReferences<T> : ActionTask<GraphReferences>
    {
        [RequiredField] public BBParameter<string> eventName;
        public BBParameter<T> value;
        
        protected override string info => $"Send {eventName.value} to all graphs\nPassing {value}";

        protected override void OnExecute() 
        {
            GraphReference[] owners = agent.Graphs;
            
            foreach (GraphReference owner in owners)
                owner.graphOwner.SendEvent(eventName.value, value.value, agent.transform);
            
            EndAction(true);
        }
    }
}