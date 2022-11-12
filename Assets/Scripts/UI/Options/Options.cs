using Sirenix.OdinInspector;
using UnityEngine;

namespace AdventureGame.UI.AppOptions
{
    public class Options : MonoBehaviour
    {
        [Title("Options")]
        [SerializeField] private VideoOptions videoOptions;
        [SerializeField] private AudioOptions audioOptions;
        [SerializeField] private ControlsOptions controlsOptions;

        public virtual void Init() // LoadOptions
        {
            videoOptions.LoadVideoSettings();
            audioOptions.LoadAudioSettings();
            controlsOptions.LoadInputSettings();
        }
    }
}