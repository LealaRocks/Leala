using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace iSOLID.RiskAdvisory.Domain.Model
{
    public class User : UserBase
    {
        public string Password { get; set; }

        public string PasswordSalt { get; set; }
        public Guid? ApiKey { get; set; }

        public UserBase ToBase()
        {
            return new UserBase()
            {
                Id = this.Id,
                Email = this.Email,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Version = this.Version
            };
        }

        public void SetPasswordAndSalt(string password)
        {
            // generate a 128-bit salt using a secure PRNG
            byte[] salt = new byte[128 / 8];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            this.Password = GetHash(password, salt);
            this.PasswordSalt = Convert.ToBase64String(salt);
        }

        public bool ValidatePassword(string password)
        {
            return this.Password == GetHash(password, Convert.FromBase64String(this.PasswordSalt));
        }

        private static string GetHash(string password, byte[] salt)
        {
            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }
    }
}
