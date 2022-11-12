using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Variables)]
    [Description("Useful to check if the object went to the pool or was destroyed")]
    public class CheckNullOrDisable : ConditionTask
    {
        [BlackboardOnly]
        public BBParameter<GameObject> variable;
        
        protected override string info
        {
            get { return variable + "is Null or Disabled"; }
        }

        protected override bool OnCheck()
        {
            return variable.isNull || !variable.value.activeSelf;
        }
    }
}