using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MalDeals.Services
{
    public class ApiKeyService
    {
        private readonly ConfigManager _config;

        public ApiKeyService(ConfigManager configManager)
        {
            _config = configManager;
        }
        public string ApiKeyGenerator(string userRole)
        {
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            var rawData = $"{userRole}:{timestamp}";

            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_config.Jwt.SecretKey));
            var hash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(rawData)))
            .Replace("+", "-")
            .Replace("/", "_")
            .Replace("=", "");

            Console.WriteLine("userRole: " + userRole);
            Console.WriteLine("timestamp: " + timestamp);
            Console.WriteLine("hash: " + hash);
            return $"{userRole}:{timestamp}:{hash}";
        }

        public bool ValidateApiKey(string apiKey, out string userRole)
        {
            userRole = string.Empty;

            var parts = apiKey.Split(':');
            if (parts.Length != 3) return false; // API-Key muss 3 Teile haben!

            userRole = parts[0];
            var timestamp = parts[1];
            var receivedHash = parts[2];

            var rawData = $"{userRole}:{timestamp}";

            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_config.Jwt.SecretKey));
            var expectedHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(rawData)))
            .Replace("+", "-")
            .Replace("/", "_")
            .Replace("=", "");

            return expectedHash == receivedHash;
        }
    }
}