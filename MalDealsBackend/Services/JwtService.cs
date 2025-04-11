using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MalDealsBackend.Services
{
    public class JwtService
    {
        private readonly byte[] _key;
        private readonly string _issuer;
        private readonly string _audience;

        public JwtService(ConfigManager configManager)
        {
            _key = Encoding.UTF8.GetBytes(configManager.Jwt.SecretKey);
            _issuer = configManager.Jwt.Issuer;
            _audience = configManager.Jwt.Audience;

        }

        public string GenerateToken(string userName, string userRole)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
            new Claim(ClaimTypes.Name, userName),
            new Claim(ClaimTypes.Role, userRole)
        }),
                Expires = DateTime.UtcNow.AddDays(14),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _issuer,
                Audience = _audience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public ClaimsPrincipal? ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var parameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(_key),
                    ValidateIssuer = !string.IsNullOrEmpty(_issuer),
                    ValidateAudience = !string.IsNullOrEmpty(_audience),
                    ValidIssuer = _issuer,
                    ValidAudience = _audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(5)
                };

                return tokenHandler.ValidateToken(token, parameters, out _);
            }
            catch
            {
                return null;
            }
        }

    }
}