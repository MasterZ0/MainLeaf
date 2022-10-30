using UnityEngine;

namespace AdventureGame.AppOptions
{
    public class Options : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private VideoOptions videoOptions;
        [SerializeField] private AudioOptions audioOptions;
        [SerializeField] private ControlsOptions controlsOptions;
        [Space]
        [SerializeField] protected GameObject firstBtn;

        public virtual void Init() // LoadOptions
        {
            return;
            videoOptions.LoadVideoSettings();
            audioOptions.LoadAudioSettings();
            controlsOptions.LoadInputSettings();
        }

        public virtual void OnOpenSettings() { }

        public virtual void OnCloseSettings() { }
    }
}