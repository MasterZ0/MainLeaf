using Sirenix.OdinInspector;
using UnityEngine;

namespace AdventureGame.MainMenu
{
    public class Outline : MonoBehaviour
    {
        [OnValueChanged(nameof(OnChangeColor))]
        [SerializeField] private Color color;
        [SerializeField] private Renderer[] renderers;

        private const string SelectionColor = "_SelectionColor";

        private void OnValidate()
        {
            OnChangeColor();
        }

        private void OnChangeColor()
        {
            MaterialPropertyBlock[] propertyBlock = new MaterialPropertyBlock[renderers.Length];

            for (int i = 0; i < renderers.Length; i++)
            {
                propertyBlock[i] = new MaterialPropertyBlock();

                renderers[i].GetPropertyBlock(propertyBlock[i]);
                propertyBlock[i].SetColor(SelectionColor, color);
                renderers[i].SetPropertyBlock(propertyBlock[i]);
            }
        }
    }
}