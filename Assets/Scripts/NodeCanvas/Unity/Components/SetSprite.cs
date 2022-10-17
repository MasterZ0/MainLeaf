using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas
{
    [Category(Categories.Components)]
    [Description("Sets a Sprite Renderer Sprite")]
    public class SetSprite : ActionTask<SpriteRenderer> 
    {
        public BBParameter<Sprite> sprite;

        protected override void OnExecute()
        {
            agent.sprite = sprite.value;
            EndAction(true);
        }
    }
}