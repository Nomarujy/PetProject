namespace Portfolio.Data.Account.Encryptor
{
    public interface IPasswordEncryptor
    {
        string EncryptPassword(string password);
        bool PasswordEqual(string password, string Hash);
    }
}