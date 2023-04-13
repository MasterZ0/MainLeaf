using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using AdventureGame.Shared.NodeCanvas;
using AdventureGame.Audio;

namespace AdventureGame.NodeCanvas.Audio
{
    [NodeCategory(Categories.Audio)]
    [NodeDescription("Stop the selected sound instance.")]
    public class StopSound : ActionTask 
    {
        /*[RequiredField]*/ public Parameter<SoundInstance> sound;
        public Parameter<bool> fadeOut;

        public override string Info => $"{name} {sound}";

        protected override void StartAction() {
            if (fadeOut.Value)
                sound.Value.StopWithFade();
            else
                sound.Value.StopImmediate();
            EndAction(true);
        }
    }
}