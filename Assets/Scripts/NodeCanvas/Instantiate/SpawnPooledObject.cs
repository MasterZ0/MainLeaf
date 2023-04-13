using AdventureGame.Shared.NodeCanvas;
using AdventureGame.ObjectPooling;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using UnityEngine;


namespace AdventureGame.NodeCanvas.Instantiate 
{
    [NodeCategory(Categories.Instantiate)]
    [NodeDescription("Get object from ObjectPool")]
    public class SpawnPooledObject<T> : ActionTask where T : Component 
    {
        [Header("Spawn Pooled Object")]
        /*[RequiredField]*/ public Parameter<T> prefab;
        public Parameter<Vector3> position = Vector3.zero;
        public Parameter<Quaternion> rotation = Quaternion.identity;
        public Parameter<Transform> parent = null;

        [Header("Out")]
        public Parameter<T> returnedObject;

        public override string Info => $"Spawn {prefab}";

        protected override void StartAction() 
        {
            returnedObject.Value = ObjectPool.SpawnPooledObject(prefab.Value, position.Value, rotation.Value, parent.Value);
            EndAction(true);
        }
    }
}