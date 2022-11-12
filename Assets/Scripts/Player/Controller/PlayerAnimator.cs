using AdventureGame.Data;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using RootMotion.FinalIK;
using System.Collections;

namespace AdventureGame.Player
{
    public enum AnimatorState // Changed animations : Jump stopped, Run/Walk forward, landing stopped
    {
        Standard,
        LoadingRequest,
        PlayingRequest
    }

    [Serializable]
    [FoldoutGroup("Animator"), HideLabel, InlineProperty]
    public class PlayerAnimator : PlayerClass
    {
        [Header("Components")]
        [SerializeField] private Animator animator;
        [SerializeField] private AimIK aimIk;

        [Header("States")]
        [SerializeField] private string jumpMoving = "Jump";
        [SerializeField] private string falling = "Falling";

        private Coroutine aimCoroutine;

        private PlayerPhysicsSettings Settings => Controller.PlayerSettings.Physics;

        internal void Falling() => PlayAllLayers(falling);
        internal void Jump() => PlayAllLayers(jumpMoving);
        
        /// <param name="transitTime">0 - 1</param>
        public void PlayAllLayers(string stateName, float transitTime = 0.25f, int layerCount = 2)
        {
            for (int layerIndex = 0; layerIndex <= layerCount; layerIndex++)
            {
                AnimatorStateInfo current = animator.GetCurrentAnimatorStateInfo(layerIndex);
                animator.CrossFade(stateName, transitTime / current.length, layerIndex);
            }
        }

        public void SetAimWeight(float weight)
        {
            if (aimCoroutine != null)
            {
                Controller.StopCoroutine(aimCoroutine);
            }

            aimCoroutine = Controller.StartCoroutine(UpdateAimWeight(weight, Settings.AimTransitionDuration));
        }

        private IEnumerator UpdateAimWeight(float weight, float transitionDuration)
        {
            IKSolver solver = aimIk.GetIKSolver();

            while (solver.IKPositionWeight != weight)
            {
                float newWeight = Mathf.MoveTowards(solver.IKPositionWeight, weight, 1f / transitionDuration * Time.fixedDeltaTime);
                solver.SetIKPositionWeight(newWeight);
                yield return new WaitForFixedUpdate();
            }
        }
    }
}