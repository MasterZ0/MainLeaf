using UnityEngine;
using Sirenix.OdinInspector;

namespace AdventureGame.Player
{

    /// <summary>
    /// Note to developers: Please describe what this MonoBehaviour does.
    /// </summary>
    public class AnimationTest : MonoBehaviour 
    {

        //[Title("AnimationTest")] // Remember to use attributes, #regions and XML Documentation :)
        public Animator animator;

        [Range(-1, 1), OnValueChanged(nameof(UpdateParameters))]
        public float velocityX;
        [Range(-1, 1), OnValueChanged(nameof(UpdateParameters))]
        public float velocityZ;
        [Range(0, 1), OnValueChanged(nameof(UpdateParameters))]
        public float moveSpeed;

        [Button]
        private void UpdateParameters()
        {
            animator.SetFloat("VelocityX", velocityX);
            animator.SetFloat("VelocityZ", velocityZ);
            animator.SetFloat("MoveSpeed", moveSpeed);
        }


        [Button]
        private void Play(string stateName, int layerIndex = 0, float transitTime = 0.25f)
        {
            AnimatorStateInfo current = animator.GetCurrentAnimatorStateInfo(layerIndex);
            animator.CrossFade(stateName, transitTime / current.length, layerIndex);
            UpdateParameters();
        }

        [Button]
        private void Idle(int layerIndex = 0)
        {
            Play("Idle", layerIndex);
        }

        [Button]
        private void Walk(int layerIndex = 0)
        {
            Play("Walk", layerIndex);
        }

        [Button]
        private void Run(int layerIndex = 0)
        {
            Play("Run", layerIndex);
        }

        [Button]
        private void Aim(int layerIndex = 0)
        {
            Play("Aim", layerIndex);
        }
    }
}