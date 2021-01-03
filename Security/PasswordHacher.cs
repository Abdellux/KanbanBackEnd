using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
namespace KanbanApi.Security
{
    public class PasswordHacher : IPasswordHacher
    {
       public string GetHashedPassword(string password) 
       {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // générer le salt en chaine de caractère
            string strSalt = Convert.ToBase64String(salt);
            string hashed = GenerateHashed(salt, password);

            //le strSalt servira lors de la vérification
            return String.Concat(hashed, ':', strSalt);
        }

        public bool checkedPassword(string hashedPassword, string password)
        {
            var hashedPasswordAndSalt = hashedPassword.Split(':');

            string hashed = hashedPasswordAndSalt[0]; 
            string strSalt = hashedPasswordAndSalt[1];

            // hacher le mot de passe inséré par le user avec le salt enregister dans la base de donnée
            string hashedPwd = GenerateHashed(Convert.FromBase64String(strSalt), password);

            return hashed.Equals(hashedPwd);
        }

        private string GenerateHashed(byte[] salt, string password)
        {
            // hacher le mot le password + le salt 
             string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            
            return hashed;
        }
    }
}