using AdventureGame.Data;
using UnityEngine;
using System.Collections;

namespace AdventureGame.Gameplay.Components
{
    public static class HitVFX
    {
        private static GeneralSettings GeneralSettings => GameSettings.General;

        public static void ApplyHitFX(this MonoBehaviour monoBehaviour, Renderer[] renderers, Material[] defaultSharedMaterial)
        {
            monoBehaviour.StartCoroutine(HitCoroutine(renderers, defaultSharedMaterial));
        }

        private static IEnumerator HitCoroutine(Renderer[] renderers, Material[] defaultSharedMaterial)
        {
            for (int i = 0; i < renderers.Length; i++)
            {
                defaultSharedMaterial[i] = renderers[i].sharedMaterial;
                renderers[i].sharedMaterial = GeneralSettings.HitMaterial;
            }

            yield return new WaitForSeconds(GeneralSettings.HitMaterialSeconds);

            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].sharedMaterial = defaultSharedMaterial[i];
            }
        }
    }
}
