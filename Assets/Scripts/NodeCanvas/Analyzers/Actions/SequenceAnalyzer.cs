using AdventureGame.Shared.NodeCanvas;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Analyzers {

    [NodeCategory(Categories.Analyzers)]
    [NodeDescription("Check how many times the last action was repeated. It will ignore the index case the limit is equals or less 0")]
    public class SequenceAnalyzer : ActionTask {

        [Header("In")]
        /*[RequiredField]*/ public Parameter<List<float>> chancesEnter;
        public Parameter<int> lastAction = -1; // -1 is the first interaction

        [Header("Config")]
        public Parameter<List<int>> actionsLimit; // Minimum must to be '1'

        [Header("Out")]
        public Parameter<List<float>> resultChance;

        private int actionAnalyzed = -1;
        private int counter;
        protected override void StartAction() {
            List<float> currentChances = new List<float>(chancesEnter.Value);

            // Ignoring the first interaction
            if (lastAction.Value < 0) {

                resultChance.Value = currentChances;
                EndAction(true);
                return;
            }

            // Check if is a different action
            if (actionAnalyzed != lastAction.Value) {
                actionAnalyzed = lastAction.Value;
                counter = 1;
            }

            // If exceed the limit, the chance is 0
            if (actionsLimit.Value[actionAnalyzed] > 0 && counter >= actionsLimit.Value[actionAnalyzed]) {
                currentChances[actionAnalyzed] = 0;
            }
            else {
                counter++;
            }

            resultChance.Value = currentChances;
            EndAction(true);
        }
    }
}