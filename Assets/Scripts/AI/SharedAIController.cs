using BehaviorDesigner.Runtime;
using System;
using UnityEngine;

namespace AI {
    [System.Serializable]
    public class SharedAIController : SharedVariable<AIController> {
        public static implicit operator SharedAIController(AIController value) { return new SharedAIController { mValue = value }; }
    }
}
