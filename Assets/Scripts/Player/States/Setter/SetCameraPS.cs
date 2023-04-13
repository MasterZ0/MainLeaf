using Z3.NodeGraph.Core;
using UnityEngine;

namespace AdventureGame.Player.States
{
    public class SetCameraPS : PlayerAction
    {
        public Parameter<GameObject> newCamera;

        public override string Info => $"{name}: {newCamera}";

        protected override void EnterState()
        {
            Camera.SwitchCamera(newCamera.Value);
            EndAction();
        }
    }
}   