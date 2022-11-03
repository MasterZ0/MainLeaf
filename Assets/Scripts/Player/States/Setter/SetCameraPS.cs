using NodeCanvas.Framework;
using UnityEngine;

namespace AdventureGame.Player.States
{
    public class SetCameraPS : PlayerAction
    {
        public BBParameter<GameObject> newCamera;

        protected override string info => $"{name}: {newCamera}";

        protected override void EnterState()
        {
            Camera.SwitchCamera(newCamera.value);
            EndAction();
        }
    }
}   