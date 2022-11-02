using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace LibraryCommon
{
    // Reference: https://stackoverflow.com/questions/52146528/how-to-validate-salted-and-hashed-password-in-c-sharp
    public static class Hashing
	{
        // Generates salt value and hashed password
        public static void GenerateSaltedHash(string password, out string hash, out string salt)
        {
            var saltBytes = new byte[64];
            var provider = new RNGCryptoServiceProvider();
            provider.GetNonZeroBytes(saltBytes);
            salt = Convert.ToBase64String(saltBytes);

            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 10000);
            hash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));
        }

        // Returns true if the entered password matches the stored hash
        public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            var saltBytes = Convert.FromBase64String(storedSalt);
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(enteredPassword, saltBytes, 10000);
            return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)) == storedHash;
        }
    }
}
