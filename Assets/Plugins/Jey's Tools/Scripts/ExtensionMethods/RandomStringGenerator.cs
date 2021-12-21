using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Plugins.Jey_s_Tools.Scripts.ExtensionMethods
{
    public static class RandomStringGenerator
    {
        public static string GenerateRandomString(int length)
        {
            if (length <= 0)
            {
                throw new Exception("Expected nonce to have positive length");
            }

            const string charset = "0123456789ABCDEFGHIJKLMNOPQRSTUVXYZabcdefghijklmnopqrstuvwxyz-._";
            var cryptographicallySecureRandomNumberGenerator = new RNGCryptoServiceProvider();
            var result = string.Empty;
            var remainingLength = length;

            var randomNumberHolder = new byte[1];
            while (remainingLength > 0)
            {
                var randomNumbers = new List<int>(16);
                for (var randomNumberCount = 0; randomNumberCount < 16; randomNumberCount++)
                {
                    cryptographicallySecureRandomNumberGenerator.GetBytes(randomNumberHolder);
                    randomNumbers.Add(randomNumberHolder[0]);
                }

                foreach (int randomNumber in randomNumbers.TakeWhile(number => remainingLength != 0)
                    .Where(randomNumber => randomNumber < charset.Length))
                {
                    result += charset[randomNumber];
                    remainingLength--;
                }
            }

            return result;
        }

        public static string GenerateSHA256NonceFromRawNonce(string rawNonce)
        {
            var sha = new SHA256Managed();
            byte[] utf8RawNonce = Encoding.UTF8.GetBytes(rawNonce);
            byte[] hash = sha.ComputeHash(utf8RawNonce);

            return hash.Aggregate(string.Empty, (current, t) => current + t.ToString("x2"));
        }
    }
}