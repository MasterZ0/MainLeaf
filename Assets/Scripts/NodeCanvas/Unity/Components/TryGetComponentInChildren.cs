using NodeCanvas.Framework;
using UnityEngine;
using ParadoxNotion.Design;
using AdventureGame.Shared.NodeCanvas;

namespace AdventureGame.NodeCanvas.Unity
{

    [Category(Categories.Components)]
    [Description("Try to get the first children with the respective class.")]
    public class TryGetComponentInChildren<T> : ConditionTask
    {
        public BBParameter<Transform> transform;
        public BBParameter<T> returnedElement;

        protected override string info => $"{name} of {transform}";
        
        protected override bool OnCheck()
        {
            returnedElement.value = transform.value.GetComponentInChildren<T>(true);
            
            if (returnedElement.value == null)
                return false;
            return true;
        }
    }
}