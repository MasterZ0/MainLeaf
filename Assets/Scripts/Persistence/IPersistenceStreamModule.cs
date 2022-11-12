namespace AdventureGame.Persistence
{
    internal interface IPersistenceStreamModule
    {
        string LoadDataFromKey(string key);
        void WriteDataToKey(string encryptedData, string key);
        void ClearDataFromKey(string key);
    }
}