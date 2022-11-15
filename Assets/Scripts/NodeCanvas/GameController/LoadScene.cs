using AdventureGame.Shared.NodeCanvas;
using AdventureGame.ApplicationManager;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace AdventureGame.NodeCanvas.GameController
{
    [Category(Categories.GameManager)]
    public class LoadScene : ActionTask
    {
        public BBParameter<GameScene> scene;

        protected override string info => $"{name}: {scene}";
        protected override void OnExecute()
        {
            GameManager.RequestLoadScene(scene.value);
            EndAction(true);
        }
    }
}