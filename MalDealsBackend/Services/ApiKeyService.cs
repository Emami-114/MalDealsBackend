using System.Security.Cryptography;
using System.Text;

namespace MalDealsBackend.Services
{
    public class ApiKeyService
    {
        private readonly ConfigManager _config;

        public ApiKeyService(ConfigManager configManager)
        {
            _config = configManager;
        }
        public string ApiKeyGenerator(string deviceId)
        {
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            var rawData = $"{deviceId}:{timestamp}";

            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_config.ApiKey.SecretKey));
            var hash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(rawData)))
            .Replace("+", "-")
            .Replace("/", "_")
            .Replace("=", "");
            return $"{deviceId}:{timestamp}:{hash}";
        }

        public bool ValidateApiKey(string? apiKey, out string deviceId)
        {
            deviceId = string.Empty;
            if (string.IsNullOrEmpty(apiKey)) { return false; }
            var parts = apiKey.Split(':');
            if (parts.Length != 3) return false; // API-Key muss 3 Teile haben!

            deviceId = parts[0];
            var timestamp = parts[1];
            var signature = parts[2];

            var rawData = $"{deviceId}:{timestamp}";

            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_config.ApiKey.SecretKey));
            var expectedHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(rawData)))
            .Replace("+", "-")
            .Replace("/", "_")
            .Replace("=", "");

            return expectedHash == signature;
        }


        public bool ValidateSignature(string deviceId, string timestamp, string signature)
        {
            string secretKey = _config.ApiKey.SecretKey;
            string data = $"{deviceId}:{timestamp}";

            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));
            byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
            string expectedSignature = Convert.ToBase64String(hash)
                .Replace("+", "-")
                .Replace("/", "_")
                .Replace("=", "");

            return expectedSignature == signature;
        }
    }
}