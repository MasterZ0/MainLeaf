using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Components)]
    [Description("Sets a sprite size based on the distance to a given point.")]
    public class SetTiledSpriteSize : ActionTask<SpriteRenderer>
    {
        public BBParameter<Transform> point;
        public BBParameter<bool> setX = false;
        public BBParameter<bool> setY = false;

        protected override string info => point.value != null ? $"Set sprite size to {point.value.position}" : "Set sprite size to NULL";

        protected override void OnExecute()
        {
            Vector2 size = agent.size;
            
            if(setX.value)
                size.x = Mathf.Abs(point.value.position.x - agent.transform.position.x);
            if(setY.value)
                size.y = Mathf.Abs(point.value.position.y - agent.transform.position.y);
            
            agent.size = size;
            EndAction(true);
        }
    }
}