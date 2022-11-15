using UnityEngine;
using Sirenix.OdinInspector;
using I2.Loc;

namespace AdventureGame.Items.Data
{
    /// <summary>
    /// Basic information of all items
    /// </summary>
    public abstract class ItemData : ScriptableObject
    {
        [Title("Item Data")]
        public LocalizedString itemName;
        public LocalizedString itemDescription;

        [VerticalGroup("Vertical")]
        [HorizontalGroup("Vertical/Horizontal")]
        [BoxGroup("Vertical/Horizontal/Icon", centerLabel: true)]
        [PreviewField(200, ObjectFieldAlignment.Center), HideLabel]
        public Sprite icon;

        [BoxGroup("Vertical/Horizontal/Image", centerLabel: true)]
        [PreviewField(200, ObjectFieldAlignment.Center), HideLabel]
        public Sprite image;

        [BoxGroup("Vertical/Horizontal/Model", centerLabel: true)]
        [PreviewField(200, ObjectFieldAlignment.Center), HideLabel]
        public GameObject model;

        public Transform collectFX;
    }
}