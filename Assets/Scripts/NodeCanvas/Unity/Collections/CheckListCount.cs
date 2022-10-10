using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion;
using ParadoxNotion.Design;
using System.Collections;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Collections)]
    [Description("Compare the list Count")]
    public class CheckListCount : ConditionTask {

        public BBParameter<IList> list;
        public CompareMethod checkType = CompareMethod.EqualTo;
        public BBParameter<int> value;
        protected override string info {
            get { return list + ".Count" + OperationTools.GetCompareString(checkType) + value; }
        }

        protected override bool OnCheck() {
            return OperationTools.Compare(list.value.Count, value.value, checkType);
        }
    }
}