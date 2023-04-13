using System.Collections.Generic;
using System.Linq;
using AdventureGame.Shared.NodeCanvas;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;

namespace AdventureGame.NodeCanvas.Utils
{
    [NodeCategory(Categories.Events)]
    [NodeDescription("Converts a graph reference to a list of graph owners")]
    public class GraphReferencesToList : ActionTask<GraphReferences>
    {
        public Parameter<List<GraphOwner>> outOwners;

        public override string Info => $"Converting References to {outOwners}";

        protected override void StartAction()
        {
            outOwners.Value = Agent.Graphs.Select(r => r.graphOwner).ToList();
            EndAction(true);
        }
    }
}