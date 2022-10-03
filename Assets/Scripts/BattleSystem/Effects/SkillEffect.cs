using Sirenix.OdinInspector;

namespace AdventureGame.BattleSystem
{
    [System.Serializable, HideReferenceObjectPicker, InlineProperty]
    public abstract class SkillEffect 
    {
        public virtual void OnEquip() { }
        public virtual void OnUnequip() { }
        public abstract void Start();
        public abstract void Update();
        public abstract void End();
        public abstract bool IsExit();

    }
}
