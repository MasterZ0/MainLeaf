namespace AdventureGame.Items
{
    /// <summary>
    /// Note to developers: Please describe what this class does.
    /// </summary>
    public interface IItemCollector 
    {
        //public bool AddItem(ItemData item, int amount); //Needs to be the instance with reference inside
        public void AddGold(int amount);
    }
}