using dotenv.net;

namespace MalDealsBackend.Services
{
    public class ConfigManager
    {
        public JwtConfig Jwt { get; private set; }
        public DbConfig Database { get; private set; }

        public ConfigManager()
        {
            // .env Datei laden
            DotEnv.Load();

            // JWT-Config initialisieren
            Jwt = new JwtConfig
            {
                SecretKey = GetEnv("JWT_SECRET_KEY", "default-secret"),
                Issuer = GetEnv("JWT_ISSUER", "default-issuer"),
                Audience = GetEnv("JWT_AUDIENCE", "default-audience"),
                TokenExpiryMinutes = int.TryParse(GetEnv("JWT_EXPIRY_MINUTES", "60"), out var expiry) ? expiry : 60
            };

            // DB-Config initialisieren
            Database = new DbConfig
            {
                ConnectionString = GetEnv("DB_CONNECTION_STRING", "Host=localhost;Database=mydb;Username=user;Password=pass"),
                DatabaseType = GetEnv("DB_TYPE", "PostgreSQL")
            };
        }

        private string GetEnv(string key, string defaultValue)
        {
            return Environment.GetEnvironmentVariable(key) ?? defaultValue;
        }
    }
}

public class JwtConfig
{
    public string SecretKey { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int TokenExpiryMinutes { get; set; }
}

public class DbConfig
{
    public string ConnectionString { get; set; } = string.Empty;
    public string DatabaseType { get; set; } = string.Empty;
}