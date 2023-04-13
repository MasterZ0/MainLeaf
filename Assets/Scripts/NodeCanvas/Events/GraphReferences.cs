using Z3.NodeGraph.Core;
using System.Linq;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Utils { 

    /// <summary>
    /// Stores a list of graph owners
    /// </summary>
    public class GraphReferences : MonoBehaviour 
    {
        [Header("GraphReferences")]
        [SerializeField] private GraphReference[] references;
        public GraphReference[] Graphs => references;

        public GraphOwner GetGraph(string name)
        {
            return references.First(r => r.keyName == name).graphOwner;
        }

        [System.Serializable]
        public struct GraphReference
        {
            public string keyName;
            public GraphOwner graphOwner;
        }
    }   
}