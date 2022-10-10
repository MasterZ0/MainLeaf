using AdventureGame.Shared.NodeCanvas;
using AdventureGame.ObjectPooling;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using Header = ParadoxNotion.Design.HeaderAttribute;

namespace AdventureGame.NodeCanvas.Instantiate {

    [Category(Categories.Instantiate)]
    [Description("Return object to ObjectPool")]
    public class ReturnToPool<T> : ActionTask where T : Component {

        [Header("Return To Pool")]
        [RequiredField] public BBParameter<T> prefab;

        protected override string info => prefab.isDefined ?
            $"Return {prefab}" : name;

        protected override void OnExecute() {
            prefab.value.ReturnToPool();
            EndAction(true);
        }
    }
}