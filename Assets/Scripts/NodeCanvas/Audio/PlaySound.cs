using AdventureGame.Shared.NodeCanvas;
using AdventureGame.Audio;
using AdventureGame.Shared.ExtensionMethods;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Audio
{
    [Category(Categories.Audio)]
    [Description("Play the current SoundData")]
    public class PlaySound : ActionTask<Transform>
    {
        public BBParameter<SoundData> soundData;
        public BBParameter<SoundInstance> instanceReturned;

        protected override string info =>
            soundData.isNoneOrNull ? "♫ Play <b>NULL</b>" :
            $"♫ Play <b>{soundData.value.name.UnderscoreByReduction()}</b>";

        protected override void OnExecute()
        {
            instanceReturned.value = soundData.value.PlaySound(agent);
            EndAction(true);
        }
    }
}