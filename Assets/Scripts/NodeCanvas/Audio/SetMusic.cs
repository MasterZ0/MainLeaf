using AdventureGame.Shared.NodeCanvas;
using AdventureGame.Audio;
using AdventureGame.Shared.ExtensionMethods;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Audio
{
    [Category(Categories.Audio)]
    [Description("Set current music")]
    public class SetMusic : ActionTask<Transform>
    {
        [OdinTree] public SoundReference soundReference;

        protected override string info => soundReference.IsNull ?
            "♫ Set Music <b>NULL</b>" :
            $"♫ Set Music <b>{soundReference.Path.StringReduction()}</b>";

        protected override void OnExecute()
        {
            AudioManager.SetCurrentMusic(soundReference);
            EndAction(true);
        }
    }
}