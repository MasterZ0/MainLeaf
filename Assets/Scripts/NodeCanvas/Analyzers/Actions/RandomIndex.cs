using AdventureGame.Shared.NodeCanvas;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Analyzers {

    [NodeCategory(Categories.Analyzers)]
    [NodeDescription("Draws the number index based on the probability of its value.")]
    public class RandomIndex : ActionTask {
                
        [Header("Out")]
        /*[RequiredField]*/ public Parameter<List<float>> actionChance;
        /*[RequiredField]*/ public Parameter<int> actionIndex = -1;

        public override string Info => actionChance.Value == null || actionChance.Value.Count == 0 ?
            $"Random Index" :
            $"Random between 0 - {actionChance.Value.Count - 1}";

        protected override void StartAction() {
            List<float> actions = actionChance.Value;

            float maxChance = actions.Sum();
            float random = Random.Range(0, maxChance);
            float counter = 0f;

            for (int i = 0; i < actions.Count; i++) {
                counter += actions[i];

                if (counter >= random) {
                    actionIndex.Value = i;
                    EndAction(true);
                    return;
                }
            }

            throw new IndexOutOfRangeException();
        }
    }
}