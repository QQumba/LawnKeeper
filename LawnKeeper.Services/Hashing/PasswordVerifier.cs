namespace LawnKeeper.Services.Hashing
{
    public class PasswordVerifier
    {
        internal static bool VerifyPassword(string password, PasswordSecret passwordSecret)
        {
            return BCrypt.Net.BCrypt.Verify(password + passwordSecret.Salt, passwordSecret.Hash);
        }
    }
}