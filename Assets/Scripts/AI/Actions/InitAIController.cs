using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BehaviorDesigner.Runtime.BehaviorManager;

namespace AI {
    [TaskCategory("AI")]
    public class InitAIController : Action {
        [RequiredField]
        public SharedAIController aiController;
        [RequiredField]
        public SharedGameObjectList waypoints;
        [RequiredField]
        public SharedFloat walkSpeed;
        [RequiredField]
        public SharedFloat sprintSpeed;

        public override TaskStatus OnUpdate() {
            waypoints.Value = EnemyGenerator.SpawPoints;
            walkSpeed.Value = aiController.Value.EnemyAttributes.walkSpeed;
            sprintSpeed.Value = aiController.Value.EnemyAttributes.sprintSpeed;
            return TaskStatus.Success;
        }
    }
}

