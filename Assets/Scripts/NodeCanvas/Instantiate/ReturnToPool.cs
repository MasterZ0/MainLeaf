using AdventureGame.Shared.NodeCanvas;
using AdventureGame.ObjectPooling;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using UnityEngine;


namespace AdventureGame.NodeCanvas.Instantiate {

    [NodeCategory(Categories.Instantiate)]
    [NodeDescription("Return object to ObjectPool")]
    public class ReturnToPool<T> : ActionTask where T : Component {

        [Header("Return To Pool")]
        /*[RequiredField]*/ public Parameter<T> prefab;

        public override string Info => prefab.isDefined ?
            $"Return {prefab}" : name;

        protected override void StartAction() {
            prefab.Value.ReturnToPool();
            EndAction(true);
        }
    }
}