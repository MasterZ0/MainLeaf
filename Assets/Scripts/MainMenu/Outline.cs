using UnityEngine;

namespace AdventureGame.MainMenu
{
    /// It is necessary to have the <see cref="OutlineCustomPass"/> component in the scene and correctly configure the objects layer
    public class Outline : MonoBehaviour
    {
        [SerializeField] private Color defaultColor;
        [SerializeField] private Renderer[] renderers;

        private const string SelectionColor = "_SelectionColor";

        private void OnValidate()
        {
            SetColor(defaultColor);
        }

        public void SetColor(Color color)
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