using Sirenix.OdinInspector;
using UnityEngine;

namespace AdventureGame.AppOptions
{
    public class Options : MonoBehaviour
    {
        [Title("Options")]
        [SerializeField] private VideoOptions videoOptions;
        [SerializeField] private AudioOptions audioOptions;
        [SerializeField] private ControlsOptions controlsOptions;

        public virtual void Init() // LoadOptions
        {
            return;
            videoOptions.LoadVideoSettings();
            audioOptions.LoadAudioSettings();
            controlsOptions.LoadInputSettings();
        }

        public virtual void OnOpenOptionsWindow() { }

        public virtual void OnCloseOptionsWindow() { }
    }
}