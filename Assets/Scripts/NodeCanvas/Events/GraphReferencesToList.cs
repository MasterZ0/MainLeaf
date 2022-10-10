using System.Collections.Generic;
using System.Linq;
using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace AdventureGame.NodeCanvas.Utils
{
    [Category(Categories.Events)]
    [Description("Converts a graph reference to a list of graph owners")]
    public class GraphReferencesToList : ActionTask<GraphReferences>
    {
        public BBParameter<List<GraphOwner>> outOwners;

        protected override string info => $"Converting References to {outOwners}";

        protected override void OnExecute()
        {
            outOwners.value = agent.Graphs.Select(r => r.graphOwner).ToList();
            EndAction(true);
        }
    }
}