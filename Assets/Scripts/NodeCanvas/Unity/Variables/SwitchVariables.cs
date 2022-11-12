using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace AdventureGame.NodeCanvas.Unity {

    [Category(Categories.Variables)]
    [Description("Switch the value variable A to B")]
    public class SwitchVariables<T> : ActionTask {

        [RequiredField] public BBParameter<T> variableA;
        [RequiredField] public BBParameter<T> variableB;

        protected override void OnExecute() {
            T aux = variableA.value;
            variableA.value = variableB.value;
            variableB.value = aux;
            EndAction(true);
        }
    }
}