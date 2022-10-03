using System;
using UnityEngine;
using UnityEngine.UI;

namespace AdventureGame.AppOptions
{
    public class Options : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private VideoOptions videoOptions;
        [SerializeField] private AudioOptions audioOptions;
        [SerializeField] private ControlsOptions controlsOptions;
        [Space]
        [SerializeField] private Button firstBtn;

        private Action closeCallback;

        public void Init() // LoadOptions
        {
            videoOptions.LoadVideoSettings();
            audioOptions.LoadAudioSettings();
            controlsOptions.LoadInputSettings();
        }

        public void OpenSettings(Action onClose)
        {
            closeCallback = onClose;
            gameObject.SetActive(true);
            firstBtn.Select();
        }

        public void OnCloseSettings()
        {
            gameObject.SetActive(false);
            closeCallback();
        }
    }
}