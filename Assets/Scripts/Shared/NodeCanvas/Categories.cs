namespace AdventureGame.Shared.NodeCanvas
{
    /// <summary>
    /// Note to developers: Please describe what this class does.
    /// </summary>
    public static class Categories 
    {
        private const string AdventureGame = "AdventureGame";

        public const string Analyzers = AdventureGame + "/Analyzers";
        public const string Audio = AdventureGame + "/Audio";
        public const string Timeline = AdventureGame + "/Timeline";
        public const string Effects = AdventureGame + "/Effects";
        public const string Events = AdventureGame + "/Events";
        public const string GameManager = AdventureGame + "/Game Manager";
        public const string Instantiate = AdventureGame + "/Instantiate";
        public const string Paths = AdventureGame + "/Paths";
        public const string Persistence = AdventureGame + "/Persistence";
        public const string Projectiles = AdventureGame + "/Projectiles";
        public const string Battle = AdventureGame + "/Battle";
        public const string Dialogue = AdventureGame + "/Dialogue";
        public const string UI = AdventureGame + "/UI";

        // AI
        public const string AI = AdventureGame + "/AI";
        public const string AIInit = AI + "/Init";
        public const string AIPath = AI + "/AIPath";

        // Player
        private const string Player = AdventureGame + "/Player";
        public const string PlayerStates = Player + "/States";
        
        // Unity
        private const string Unity = AdventureGame + "/Unity";
        public const string Animations = Unity + "/Animations";
        public const string Rigidbody = Unity + "/Rigidbody";
        public const string Variables = Unity + "/Variables";
        public const string Collections = Unity + "/Collections";
        public const string Components = Unity + "/Components";
        public const string Transform = Unity + "/Transform";
        public const string Movement = Unity + "/Movement";
        public const string Physics = Unity + "/Physics";
    }
}