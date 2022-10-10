using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [System.Flags]
    public enum ItemListState
    {
        Null = 1,
        Disabled = 2,
        Enabled = 4
    }

    [Category(Categories.Collections)]
    [Description("Remove GameObjects disabled, null or enabled. Useful to remove object references that have returned to the object pool for example.")]
    public class UpdateList<T> : ActionTask where T : Component
    {
        [RequiredField] public BBParameter<List<T>> list;
        public BBParameter<ItemListState> itemState = ItemListState.Disabled | ItemListState.Null;

        protected override void OnExecute()
        {
            if (itemState.value == ItemListState.Disabled)
            {
                list.value.RemoveAll(i => !i.gameObject.activeSelf);
            }
            if (itemState.value == ItemListState.Enabled)
            {
                list.value.RemoveAll(i => i.gameObject.activeSelf);
            }
            if (itemState.value == ItemListState.Null)
            {
                list.value.RemoveAll(i => i == null);
            }

            EndAction(true);
        }
    }
}