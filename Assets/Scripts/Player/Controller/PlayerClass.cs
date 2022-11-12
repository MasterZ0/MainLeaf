namespace AdventureGame.Player
{
    public abstract class PlayerClass
    {
        protected PlayerController Controller { get; set; }

        public virtual void Init(PlayerController controller)
        {
            Controller = controller;
        }
    }
}