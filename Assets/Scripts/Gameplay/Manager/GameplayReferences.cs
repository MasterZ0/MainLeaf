using System;
using System.Collections.Generic;

namespace AdventureGame.Gameplay
{
    public static class GameplayReferences
    {
        #region References and Properties
        public static GameController Controller { get; private set; }
        public static bool InputActive { get; private set; } = true;
        #endregion

        #region Events
        public static event Action<IPlayer> OnPlayerDeath = delegate { };
        public static event Action<bool> OnPlayerInputSet = delegate { };
        #endregion

        #region Privade Variables
        private readonly static List<IPlayer> Players = new List<IPlayer>();
        private readonly static List<IControlInput> InputBlocker = new List<IControlInput>();
        #endregion

        #region Public Methods
        public static void SetReferences(GameController controller) // Injection
        {
            Controller = controller;
        }

        public static void RegisterPlayer(IPlayer player)
        {
            Players.Add(player);
        }

        public static void UnregisterPlayer(IPlayer player)
        {
            Players.Remove(player);
            OnPlayerDeath.Invoke(player);
        }

        public static void Reset()
        {
            InputActive = true;
            InputBlocker.Clear();
            Players.Clear();
        }

        public static void SetActivePlayerInput(bool enable, IControlInput locker)
        {
            if (enable)
            {
                InputBlocker.Remove(locker);
            }
            else
            {
                InputBlocker.Add(locker);
            }

            InputActive = InputBlocker.Count == 0;
            OnPlayerInputSet(InputActive);
        }
        #endregion
    }
}