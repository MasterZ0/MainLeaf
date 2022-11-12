using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Collections.Generic;

namespace AdventureGame.NodeCanvas.Analyzers {

    [Category(Categories.Analyzers)]
    [Description("Check how many times the last action was repeated. It will ignore the index case the limit is equals or less 0")]
    public class SequenceAnalyzer : ActionTask {

        [Header("In")]
        [RequiredField] public BBParameter<List<float>> chancesEnter;
        public BBParameter<int> lastAction = -1; // -1 is the first interaction

        [Header("Config")]
        public BBParameter<List<int>> actionsLimit; // Minimum must to be '1'

        [Header("Out")]
        public BBParameter<List<float>> resultChance;

        private int actionAnalyzed = -1;
        private int counter;
        protected override void OnExecute() {
            List<float> currentChances = new List<float>(chancesEnter.value);

            // Ignoring the first interaction
            if (lastAction.value < 0) {

                resultChance.value = currentChances;
                EndAction(true);
                return;
            }

            // Check if is a different action
            if (actionAnalyzed != lastAction.value) {
                actionAnalyzed = lastAction.value;
                counter = 1;
            }

            // If exceed the limit, the chance is 0
            if (actionsLimit.value[actionAnalyzed] > 0 && counter >= actionsLimit.value[actionAnalyzed]) {
                currentChances[actionAnalyzed] = 0;
            }
            else {
                counter++;
            }

            resultChance.value = currentChances;
            EndAction(true);
        }
    }
}