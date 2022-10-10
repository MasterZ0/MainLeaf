using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity {

    [Category(Categories.Movement)]
    [Description("Move the agent to a random position within the limits and at a minimum distance from the target")]
    public class TeleportLimit : ActionTask<Transform> {    // Amon Skill

        [RequiredField] public BBParameter<Transform> target;
        [RequiredField] public BBParameter<Transform> leftLimit;
        [RequiredField] public BBParameter<Transform> rightLimit;
        [RequiredField] public BBParameter<float> minDistance;

        private Vector2 limitX;
        protected override void OnExecute() {
            limitX = new Vector2(leftLimit.value.position.x, rightLimit.value.position.x);

            Randomize();
        }

        protected override void OnUpdate() {
            // This will prevent stackoverflow
            Randomize();
        }

        private void Randomize() {
            float x = Random.Range(limitX.x, limitX.y);
            float distance = Mathf.Abs(x - target.value.position.x);

            if (distance > minDistance.value) {
                agent.position = new Vector2(x, agent.position.y);
                EndAction(true);
            }
        }
    }
}