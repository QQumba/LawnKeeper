using LawnKeeper.Domain.Entities;

namespace LawnKeeper.Services.Hashing
{
    internal class PasswordSecret
    {
        private PasswordSecret(string hash, string salt)
        {
            Hash = hash;
            Salt = salt;
        }

        public PasswordSecret(User user)
        {
            Hash = user.PasswordHash;
            Salt = user.Salt;
        }
        
        public string Hash { get; }
        public string Salt { get; }
        
        public static PasswordSecret Create(string password)
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            var hash = BCrypt.Net.BCrypt.HashPassword(password + salt);
            return new PasswordSecret(hash, salt);
        }
    }
}