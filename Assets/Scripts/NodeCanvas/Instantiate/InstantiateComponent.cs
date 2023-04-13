using AdventureGame.Shared.NodeCanvas;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using UnityEngine;


namespace AdventureGame.NodeCanvas.Instantiate
{
    [NodeCategory(Categories.Instantiate)]
    [NodeDescription("This class allows to use Quaterion")]
    public class InstantiateComponent<T> : ActionTask where T : Component
    {
        [Header("Spawn Pooled Object")]
        /*[RequiredField]*/ public Parameter<T> prefab;
        public Parameter<Vector3> position = Vector3.zero;
        public Parameter<Quaternion> rotation = Quaternion.identity;
        public Parameter<Transform> parent = null;

        [Header("Out")]
        public Parameter<T> returnedObject;

        public override string Info => prefab.Value == null ?
            name : $"Instantiate {prefab}";

        protected override void StartAction()
        {
            returnedObject = Object.Instantiate(prefab.Value, position.Value, rotation.Value, parent.Value);
            EndAction(true);
        }
    }
}