using dotenv.net;
using MalDealsBackend.Models;

namespace MalDealsBackend.Services
{
    public class ConfigManager
    {
        public ConfigModel.JwtConfig Jwt { get; private set; }
        public ConfigModel.DbConfig Database { get; private set; }
        public ConfigModel.ApiKeyConfig ApiKey { get; private set; }

        public ConfigManager()
        {
            // .env Datei laden
            DotEnv.Load();

            // JWT-Config initialisieren
            Jwt = new ConfigModel.JwtConfig
            {
                SecretKey = GetEnv("JWT_SECRET_KEY", "default-secret"),
                Issuer = GetEnv("JWT_ISSUER", "default-issuer"),
                Audience = GetEnv("JWT_AUDIENCE", "default-audience"),
                TokenExpiryMinutes = int.TryParse(GetEnv("JWT_EXPIRY_MINUTES", "60"), out var expiry) ? expiry : 60
            };

            // DB-Config initialisieren
            Database = new ConfigModel.DbConfig
            {
                ConnectionString = GetEnv("DB_CONNECTION_STRING", "Host=localhost;Database=mydb;Username=user;Password=pass"),
                DatabaseType = GetEnv("DB_TYPE", "PostgreSQL")
            };

            ApiKey = new ConfigModel.ApiKeyConfig
            {
                SecretKey = GetEnv("API_KEY_SECRET_KEY","default-secret")
            };

        }

        private string GetEnv(string key, string defaultValue)
        {
            return Environment.GetEnvironmentVariable(key) ?? defaultValue;
        }
    }
}