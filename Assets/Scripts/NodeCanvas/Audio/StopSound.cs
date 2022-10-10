using NodeCanvas.Framework;
using ParadoxNotion.Design;
using AdventureGame.Shared.NodeCanvas;
using AdventureGame.Audio;

namespace AdventureGame.NodeCanvas.Audio
{
    [Category(Categories.Audio)]
    [Description("Stop the selected sound instance.")]
    public class StopSound : ActionTask 
    {
        [RequiredField] public BBParameter<SoundInstance> sound;
        public BBParameter<bool> fadeOut;

        protected override string info => $"{name} {sound}";

        protected override void OnExecute() {
            if (fadeOut.value)
                sound.value.StopWithFade();
            else
                sound.value.StopImmediate();
            EndAction(true);
        }
    }
}