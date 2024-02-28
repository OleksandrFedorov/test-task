using System.Security.Cryptography;
using System.Text;

namespace TestTask.Helpers
{
    public static class EncryptionHelper
    {
        public static string HashPasword(string password)
        {
            var byteHash = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            var hash = Encoding.UTF8.GetString(byteHash);
            return hash;
        }
    }
}
