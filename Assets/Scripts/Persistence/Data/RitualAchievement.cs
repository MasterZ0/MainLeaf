using System;

namespace AdventureGame.NodeCanvas.Rituals
{
    [Serializable]
    public class RitualAchievement
    {
        public bool BloodRitualCompleted;
        public bool WheelRitualCompleted;
        public bool StoneRitualCompleted;
        public bool CultistRitualCompleted;

        public bool AllRitualsAreCompleted => BloodRitualCompleted 
                                              && WheelRitualCompleted 
                                              && StoneRitualCompleted 
                                              && CultistRitualCompleted;
    }
}