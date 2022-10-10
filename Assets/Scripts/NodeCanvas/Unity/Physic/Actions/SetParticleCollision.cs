using System;
using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using static NodeCanvas.Tasks.Actions.SetBoolean;
using static UnityEngine.ParticleSystem;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Physics)]
    [Description("Sets particle collision message state.")]
    public class SetParticleCollision : ActionTask<ParticleSystem>
    {
        public BBParameter<BoolSetModes> setTo = BoolSetModes.True;

        protected override void OnExecute()
        {
            CollisionModule collision = agent.collision;
            
            collision.sendCollisionMessages = setTo.value switch
            {
                BoolSetModes.False => false,
                BoolSetModes.True => true,
                BoolSetModes.Toggle => !collision.sendCollisionMessages,
                _ => throw new ArgumentOutOfRangeException()
            };

            EndAction(true);
        }
    }
}