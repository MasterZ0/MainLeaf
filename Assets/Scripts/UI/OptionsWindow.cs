using AdventureGame.AppOptions;
using AdventureGame.Shared;
using AdventureGame.UI.Window;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AdventureGame.UI
{
    public class OptionsWindow : Options, IWindow
    {
        [Title("Window")]
        [SerializeField] private GameObject firstBtn;
        [Space]
        [SerializeField] private GameEvent onOpenOptions;

        public GameObject FirstGameObject => firstBtn;

        public override void Init()
        {
            base.Init();

            onOpenOptions += OnOpenOptionsWindow;
        }

        private void OnDestroy()
        {
            onOpenOptions -= OnOpenOptionsWindow;
        }

        public override void OnOpenOptionsWindow() => this.RequestOpenWindow();
        public override void OnCloseOptionsWindow() => this.TryCloseWindow();

        public void OpenWindow() => gameObject.SetActive(true);
        public void CloseWindow() => gameObject.SetActive(false);
    }
}