using UnityEngine;
using System.Collections;

namespace AdventureGame.Player
{
    public class PlayerEvents : MonoBehaviour
    {
        private PlayerArsenal Arsenal => Controller.Arsenal;

        private PlayerController Controller { get; set; }

        public void Init(PlayerController controller)
        {
            Controller = controller;
        }

        public void OnPrepareArrow() => Controller.SFX.PrepareArrow();

        public void OnShootArrow()
        {
            Controller.VFX.ShootArrow();
            Controller.SFX.ShootArrow();

            StartCoroutine(ShootAwait());
        }

        private IEnumerator ShootAwait() // IK Bug
        {
            yield return new WaitForFixedUpdate();
            Arsenal.ShootArrow();
        }
    }
}
