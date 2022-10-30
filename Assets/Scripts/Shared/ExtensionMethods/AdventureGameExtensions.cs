using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace AdventureGame.Shared.ExtensionMethods
{
    /// <summary>
    /// General Extensions
    /// </summary>
    public static class AdventureGameExtensions
    {
        /// <summary> Prevents Event system bugs </summary>
        public static void SelectWithDelay(this Selectable selectable) // Prevents Event system bugs
        {
            selectable.StartCoroutine(DelaySelect(selectable));
        }

        private static IEnumerator DelaySelect(Selectable selectable)
        {
            yield return new WaitForEndOfFrame();
            selectable.Select();
        }
    }
}