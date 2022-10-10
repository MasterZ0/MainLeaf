using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace AdventureGame.NodeCanvas.Analyzers {

    [Category(Categories.Analyzers)]
    [Description("Draws the number index based on the probability of its value.")]
    public class RandomIndex : ActionTask {
                
        [Header("Out")]
        [RequiredField] public BBParameter<List<float>> actionChance;
        [RequiredField] public BBParameter<int> actionIndex = -1;

        protected override string info => actionChance.value == null || actionChance.value.Count == 0 ?
            $"Random Index" :
            $"Random between 0 - {actionChance.value.Count - 1}";

        protected override void OnExecute() {
            List<float> actions = actionChance.value;

            float maxChance = actions.Sum();
            float random = Random.Range(0, maxChance);
            float counter = 0f;

            for (int i = 0; i < actions.Count; i++) {
                counter += actions[i];

                if (counter >= random) {
                    actionIndex.value = i;
                    EndAction(true);
                    return;
                }
            }

            throw new IndexOutOfRangeException();
        }
    }
}