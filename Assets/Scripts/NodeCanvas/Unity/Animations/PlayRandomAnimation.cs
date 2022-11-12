using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Animations)]
    [Description("Play animation by random state name")]
    public class PlayRandomAnimation : ActionTask<Animator>
    {
        public BBParameter<bool> waitUntilFinish;
        public BBParameter<List<string>> stateNames = new List<string>() { 
            "Action A",
            "Action B"
        };
        protected override string info
        {
            get
            {
                if (stateNames.isNoneOrNull)
                    return name;

                string info = waitUntilFinish.value ? 
                    $"► Playing Random " :
                    $"► Play Random ";

                if (stateNames.isDefined)                
                    info += stateNames;                
                else                
                    info += $"<b>Count = {stateNames.value.Count}</b>";
                
                return info;
            }
        }

        private AnimatorStateInfo stateInfo;
        private bool played;
        private string selectedStateName;
        protected override void OnExecute()
        {
            played = false; 
            
            int index = Random.Range(0, stateNames.value.Count);
            selectedStateName = stateNames.value[index];
            agent.Play(selectedStateName);

            if (!waitUntilFinish.value)
            {
                EndAction(true);
            }
        }

        protected override void OnUpdate()
        {

            stateInfo = agent.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.IsName(selectedStateName))
            {

                played = true;
                if (elapsedTime >= (stateInfo.length / agent.speed))
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