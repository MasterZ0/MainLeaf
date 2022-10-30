using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;

namespace AdventureGame.Player
{
    public class HUD : MonoBehaviour
    {
        [Title("HUD")]
        [SerializeField] private Slider lifeBar;

        [Header(" - Texts")]
        [SerializeField] private TMP_Text ammoCountText;

        private void UpdateLife(float percentage)
        {
            lifeBar.value = percentage;
        }

        private void UpdateAmmoCount(int ammo)
        {
            ammoCountText.text = $"{ammo} x";
        }
    }
}
