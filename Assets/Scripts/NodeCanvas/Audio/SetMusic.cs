using AdventureGame.Shared.NodeCanvas;
using AdventureGame.Audio;
using AdventureGame.Shared.ExtensionMethods;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Audio
{
    [NodeCategory(Categories.Audio)]
    [NodeDescription("Set current music")]
    public class SetMusic : ActionTask<Transform>
    {
        public SoundReference soundReference;

        public override string Info => soundReference.IsNull ?
            "♫ Set Music <b>NULL</b>" :
            $"♫ Set Music <b>{soundReference.Path.StringReduction()}</b>";

        protected override void StartAction()
        {
            AudioManager.SetCurrentMusic(soundReference);
            EndAction(true);
        }
    }
}