using dotenv.net;
using MalDealsBackend.Models;

namespace MalDealsBackend.Services
{
    public class ConfigManager
    {
        public ConfigModel.JwtConfig Jwt { get; private set; }
        public ConfigModel.DbConfig Database { get; private set; }
        public ConfigModel.ApiKeyConfig ApiKey { get; private set; }

        public ConfigModel.MinioConfig Minio {get; private set; }
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
                TokenExpiryMinutes = int.TryParse(GetEnv("JWT_EXPIRY_MINUTES", "20160"), out var expiry) ? expiry : 20160
            };

            // DB-Config initialisieren
            Database = new ConfigModel.DbConfig
            {
                ConnectionString = GetEnv("DATABASE_URL", "default_connection"),
                DatabaseType = GetEnv("DB_TYPE", "PostgreSQL")
            };

            ApiKey = new ConfigModel.ApiKeyConfig
            {
                SecretKey = GetEnv("API_KEY_SECRET_KEY","default-secret")
            };

            Minio = new ConfigModel.MinioConfig {
                AccessKey = GetEnv("MINIO_ROOT_USER","default"),
                SecretKey = GetEnv("MINIO_ROOT_PASSWORD","default")
            };

        }

        private string GetEnv(string key, string defaultValue)
        {
            return Environment.GetEnvironmentVariable(key) ?? defaultValue;
        }
    }
}