using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AI {
    [Action("Game/Reposition")]
    public class Reposition : BasePrimitiveAction {

        public override void OnStart() {
            base.OnStart();
        }

        public override TaskStatus OnUpdate() {
            return TaskStatus.RUNNING;
        }
    }


}