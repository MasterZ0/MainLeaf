

namespace AdventureGame.BattleSystem
{
    [System.Serializable/*, HideReferenceObjectPicker, InlineProperty*/]
    public abstract class SkillEffect 
    {
        public abstract void Start();
        public abstract bool Update();
        public abstract void Dispose();
    }
}
