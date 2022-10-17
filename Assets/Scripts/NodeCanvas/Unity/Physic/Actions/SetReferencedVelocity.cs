using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity 
{    
    [Category(Categories.Rigidbody)]
    [Description("Set Rigidbody velocity based on Transform direction")]
    public class SetReferencedVelocity : ActionTask<Rigidbody> 
    {        
        [RequiredField] public BBParameter<Vector3> velocity;
        protected override string info => $"Referenced velocity = {velocity}";
        protected override void OnExecute() 
        {
            agent.velocity = new Vector3()
            {
                x = agent.transform.right.x * velocity.value.x,
                y = agent.transform.up.y * velocity.value.y,
                z = agent.transform.forward.z * velocity.value.z
            };
            EndAction(true);
        }
    }
}