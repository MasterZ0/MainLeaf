using AdventureGame.Shared;
using AdventureGame.UI.AppOptions;
using AdventureGame.UI.Window;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AdventureGame.UI
{
    public class OptionsWindow : Options, IWindow
    {
        [Title("Window")]
        [SerializeField] private GameEvent onOpenOptions;

        public GameObject FirstGameObject => null; // TabGroup?

        public override void Init()
        {
            base.Init();

            onOpenOptions += OnOpenOptionsWindow;
        }

        private void OnDestroy()
        {
            onOpenOptions -= OnOpenOptionsWindow;
        }

        public void OnOpenOptionsWindow() => this.RequestOpenWindow();
        public void OnCloseOptionsWindow() => this.TryCloseWindow();

        public void OpenWindow() => gameObject.SetActive(true);
        public void CloseWindow() => gameObject.SetActive(false);
    }
}