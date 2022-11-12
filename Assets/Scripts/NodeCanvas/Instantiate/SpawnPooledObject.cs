using AdventureGame.Shared.NodeCanvas;
using AdventureGame.ObjectPooling;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using Header = ParadoxNotion.Design.HeaderAttribute;

namespace AdventureGame.NodeCanvas.Instantiate 
{
    [Category(Categories.Instantiate)]
    [Description("Get object from ObjectPool")]
    public class SpawnPooledObject<T> : ActionTask where T : Component 
    {
        [Header("Spawn Pooled Object")]
        [RequiredField] public BBParameter<T> prefab;
        public BBParameter<Vector3> position = Vector3.zero;
        public BBParameter<Quaternion> rotation = Quaternion.identity;
        public BBParameter<Transform> parent = null;

        [Header("Out")]
        public BBParameter<T> returnedObject;

        protected override string info => $"Spawn {prefab}";

        protected override void OnExecute() 
        {
            returnedObject.value = ObjectPool.SpawnPooledObject(prefab.value, position.value, rotation.value, parent.value);
            EndAction(true);
        }
    }
}