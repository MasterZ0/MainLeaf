using AdventureGame.Shared.NodeCanvas;
using AdventureGame.ApplicationManager;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;

namespace AdventureGame.NodeCanvas.GameController
{
    [NodeCategory(Categories.GameManager)]
    public class LoadScene : ActionTask
    {
        public Parameter<GameScene> scene;

        public override string Info => $"{name}: {scene}";
        protected override void StartAction()
        {
            GameManager.RequestLoadScene(scene.Value);
            EndAction(true);
        }
    }
}