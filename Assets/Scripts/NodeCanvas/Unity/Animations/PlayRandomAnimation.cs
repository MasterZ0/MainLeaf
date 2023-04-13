using AdventureGame.Shared.NodeCanvas;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [NodeCategory(Categories.Animations)]
    [NodeDescription("Play animation by random state name")]
    public class PlayRandomAnimation : ActionTask<Animator>
    {
        public Parameter<bool> waitUntilFinish;
        public Parameter<List<string>> stateNames = new List<string>() { 
            "Action A",
            "Action B"
        };
        public override string Info
        {
            get
            {
                if (stateNames.isNoneOrNull)
                    return name;

                string info = waitUntilFinish.Value ? 
                    $"► Playing Random " :
                    $"► Play Random ";

                if (stateNames.isDefined)                
                    info += stateNames;                
                else                
                    info += $"<b>Count = {stateNames.Value.Count}</b>";
                
                return info;
            }
        }

        private AnimatorStateInfo stateInfo;
        private bool played;
        private string selectedStateName;
        protected override void StartAction()
        {
            played = false; 
            
            int index = Random.Range(0, stateNames.Value.Count);
            selectedStateName = stateNames.Value[index];
            Agent.Play(selectedStateName);

            if (!waitUntilFinish.Value)
            {
                EndAction(true);
            }
        }

        protected override void UpdateAction()
        {

            stateInfo = Agent.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.IsName(selectedStateName))
            {

                played = true;
                if (NodeRunningTime >= (stateInfo.length / Agent.speed))
                {
                    EndAction(true);
                }
            }
            else if (played)
            {
                EndAction(true);
            }
        }
    }
}