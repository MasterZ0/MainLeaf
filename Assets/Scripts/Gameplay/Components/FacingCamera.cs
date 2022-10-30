using AdventureGame.Shared.ExtensionMethods;
using UnityEngine;

namespace AdventureGame.Gameplay
{
    /// <summary>
    /// Note to developers: Please describe what this MonoBehaviour does.
    /// </summary>
    public class FacingCamera : MonoBehaviour 
    {
        private Vector3 Target => MainCamera.Instance.transform.position;

        private void Update()
        {
            transform.LookAtY(Target);
        }
    }
}