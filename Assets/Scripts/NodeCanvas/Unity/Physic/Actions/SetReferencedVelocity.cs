using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity 
{    
    [Category(Categories.Rigidbody2D)]
    [Description("Set Rigidbody2D velocity based on Transform direction")]
    public class SetReferencedVelocity : ActionTask<Rigidbody2D> 
    {        
        [RequiredField] public BBParameter<Vector2> velocity;
        protected override string info => $"Referenced velocity = {velocity}";
        protected override void OnExecute() 
        {
            agent.velocity = new Vector2()
            {
                x = agent.transform.right.x * velocity.value.x,
                y = agent.transform.up.y * velocity.value.y
            };
            EndAction(true);
        }
    }
}