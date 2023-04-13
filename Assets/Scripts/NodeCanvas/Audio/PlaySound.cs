using AdventureGame.Shared.NodeCanvas;
using AdventureGame.Audio;
using AdventureGame.Shared.ExtensionMethods;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Audio
{
    [NodeCategory(Categories.Audio)]
    [NodeDescription("Play the current SoundData")]
    public class PlaySound : ActionTask<Transform>
    {
        public Parameter<SoundData> soundData;
        public Parameter<SoundInstance> instanceReturned;

        public override string Info =>
            soundData.isNoneOrNull ? "♫ Play <b>NULL</b>" :
            $"♫ Play <b>{soundData.Value.name.StringReduction()}</b>";

        protected override void StartAction()
        {
            instanceReturned.Value = soundData.Value.PlaySound(Agent);
            EndAction(true);
        }
    }
}