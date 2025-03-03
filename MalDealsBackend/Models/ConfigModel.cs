using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MalDealsBackend.Models
{
    public class ConfigModel
    {
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

        public class ApiKeyConfig
        {
            public string SecretKey { get; set; } = string.Empty;
        }
    }
}