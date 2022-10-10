using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Collections)]
    [Description("Useful to check if the object went to the pool or was destroyed")]
    public class RemoveNullOrDisableList<T> : ActionTask where T : Component
    {
        [BlackboardOnly]
        public BBParameter<List<T>> list;

        protected override string info => $"Remove Null or Disabled from {list}";
        
        protected override void OnExecute()
        {
            List<T> auxList = new List<T>(list.value);
            foreach (var item in auxList)
            {
                if (item == null || !item.gameObject.activeSelf)
                    list.value.Remove(item);
            }

            EndAction(true);
        }
    }
}