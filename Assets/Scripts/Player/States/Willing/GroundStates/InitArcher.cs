using AdventureGame.Data;
using NodeCanvas.Framework;

namespace AdventureGame.Player.States
{
    public abstract class InitPlayerPS : PlayerAction
    {
        public BBParameter<float> aimMoveSpeed;
        public BBParameter<float> walkSpeed;
        public BBParameter<float> sprintSpeed;

        protected sealed override void EnterState()
        {
            PlayerSettings settings = Init();
            agent.Setup(settings);

            aimMoveSpeed.value = settings.Physics.AimMoveSpeed;
            walkSpeed.value = settings.Physics.WalkSpeed;
            sprintSpeed.value = settings.Physics.SprintSpeed;

            EndAction(true);
        }

        protected abstract PlayerSettings Init();
    }

    public class InitArcherPS : InitPlayerPS
    {
        protected override PlayerSettings Init()
        {
            return GameSettings.Players.Archer;
        }
    }
    public class InitWarriorPS : InitPlayerPS
    {
        protected override PlayerSettings Init()
        {
            return GameSettings.Players.Warrior;
        }
    }
    public class InitMagePS: InitPlayerPS
    {
        protected override PlayerSettings Init()
        {
            return GameSettings.Players.Mage;
        }
    }
}