using AdventureGame.Shared.NodeCanvas;
using AdventureGame.ObjectPooling;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using Header = ParadoxNotion.Design.HeaderAttribute;

namespace AdventureGame.NodeCanvas.Instantiate {

    [Category(Categories.Instantiate)]
    [Description("Please describe what this ActionTask does.")]
    public class SpawnSequencer : ActionTask {

        [Header("Prefab")]
        [RequiredField] public BBParameter<Component> prefab;
        public BBParameter<Vector3> position;
        public BBParameter<Quaternion> rotation;
        public BBParameter<Transform> parent;

        [Header("Config")]
        public BBParameter<Vector3> offset;
        [MinValue(1)]
        public BBParameter<int> count = 3;
        public BBParameter<Vector2> spacing;
        public BBParameter<float> delay;

        private Vector2 currentPosition;
        private int spawned;

        protected override string info => $"Spawn Sequence {prefab}";
        protected override void OnExecute() {

            // Reset
            currentPosition = position.value + offset.value;
            if (delay.value > 0) {
                spawned = 0;
                return;
            }

            // Spawn all
            for (int i = 0; i < count.value; i++) {
                ObjectPool.SpawnPooledObject(prefab.value, currentPosition, rotation.value, parent.value);
                currentPosition += spacing.value;
            }

            EndAction(true);
        }

        protected override void OnUpdate() {

            // Time to spawn
            if (elapsedTime >= delay.value * spawned) {
                ObjectPool.SpawnPooledObject(prefab.value, currentPosition, rotation.value, parent.value);
                currentPosition += spacing.value;
                spawned++;

                if (spawned == count.value) {
                    EndAction(true);
                }
            }
        }

        public override void OnDrawGizmosSelected() {

            Vector2 currentPosition = position.value + offset.value;
            Gizmos.color = Color.red;

            for (int i = 0; i < count.value; i++) {
                Gizmos.DrawWireSphere(currentPosition, .2f);
                currentPosition += spacing.value;
            }
        }
    }
}