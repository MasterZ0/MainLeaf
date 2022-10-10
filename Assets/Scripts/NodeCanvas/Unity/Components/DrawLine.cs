using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.AI
{

    [Category(Categories.Components)]
    [Description("Draws a straight line between two local positions, it requires a LineRenderer.")]
    public class DrawLine : ActionTask
    {
        [RequiredField] public BBParameter<Vector3> startPosition;
        [RequiredField] public BBParameter<Vector3> endPosition;
        [RequiredField] public BBParameter<LineRenderer> line;

        protected override void OnExecute() {
            Draw();
            EndAction(true);
        }

        private void Draw()
        {
            line.value.SetPosition(0, startPosition.value);
            line.value.SetPosition(1, endPosition.value);
        }
    }
}