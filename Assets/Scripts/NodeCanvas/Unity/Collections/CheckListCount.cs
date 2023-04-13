using AdventureGame.Shared.NodeCanvas;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using System.Collections;

namespace AdventureGame.NodeCanvas.Unity
{
    [NodeCategory(Categories.Collections)]
    [NodeDescription("Compare the list Count")]
    public class CheckListCount : ConditionTask {

        public Parameter<IList> list;
        public CompareMethod checkType = CompareMethod.EqualTo;
        public Parameter<int> value;
        public override string Info {
            get { return list + ".Count" + OperationTools.GetCompareString(checkType) + value; }
        }

        public override bool CheckCondition() {
            return OperationTools.Compare(list.Value.Count, value.Value, checkType);
        }
    }
}