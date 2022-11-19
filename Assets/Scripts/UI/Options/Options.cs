using Sirenix.OdinInspector;
using UnityEngine;

namespace AdventureGame.UI.AppOptions
{
    public class Options : MonoBehaviour
    {
        [Title("Options")]
        [SerializeField] private AccessibilityOptions accessibilityOptions;
        [SerializeField] private VideoOptions videoOptions;
        [SerializeField] private AudioOptions audioOptions;
        [SerializeField] private ControlsOptions controlsOptions;

        public virtual void Init() // LoadOptions
        {
            accessibilityOptions.LoadSettings();
            videoOptions.LoadSettings();
            audioOptions.LoadSettings();
            controlsOptions.LoadSettings();
        }
    }
}