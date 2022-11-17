using UnityEngine;

namespace AdventureGame.DevTools
{
    public class UIVisibilityTool : MonoBehaviour
    {
        [SerializeField] private CanvasGroup[] uiGroup;

        private void Awake()
        {
            OnUpdateUI();
            ApplicationTools.OnToggleUI += OnUpdateUI;
        }

        private void OnDestroy()
        {
            ApplicationTools.OnToggleUI -= OnUpdateUI;
        }

        private void OnUpdateUI()
        {
            float newAlpha = ApplicationTools.ShowingUI ? 1f : 0f;

            foreach (CanvasGroup canvasGroup in uiGroup)
            {
                canvasGroup.alpha = newAlpha;
            }
        }
    }
}
