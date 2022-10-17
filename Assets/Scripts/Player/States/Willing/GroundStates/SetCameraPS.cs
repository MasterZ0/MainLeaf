using NodeCanvas.Framework;

namespace AdventureGame.Player.States
{
    public class SetCameraPS : PlayerAction
    {
        public BBParameter<CameraType> cameraType;

        protected override string info => $"{name}: {cameraType}";

        protected override void EnterState()
        {
            Camera.SwitchCamera(cameraType.value);
            EndAction();
        }
    }
}   