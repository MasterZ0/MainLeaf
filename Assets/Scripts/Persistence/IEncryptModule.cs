namespace AdventureGame.Persistence
{
    internal interface IEncryptModule
    {
        string Encrypt<T>(T data);
        T Decrypt<T>(string data);
    }
}