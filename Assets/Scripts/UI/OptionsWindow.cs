using AdventureGame.AppOptions;
using AdventureGame.UI.Window;
using UnityEngine;

namespace AdventureGame.UI
{
    public class OptionsWindow : Options, IWindow
    {
        public GameObject FirstGameObject => firstBtn;

        public void OpenWindow() => gameObject.SetActive(true);
        public void CloseWindow() => gameObject.SetActive(false);
        public override void OnCloseSettings() => this.TryCloseWindow();
    }
}