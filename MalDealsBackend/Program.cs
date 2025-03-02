using System.Text;
using MalDealsBackend.Data;
using MalDealsBackend.Middleware;
using MalDealsBackend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var configManager = new ConfigManager();
var apiKeyService = new ApiKeyService(configManager);
builder.Services.AddSingleton(configManager);
builder.Services.AddSwaggerConfiguration();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = configManager.Jwt.Issuer,
        ValidAudience = configManager.Jwt.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configManager.Jwt.SecretKey))
    };
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddAuthorization();

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseNpgsql(configManager.Database.ConnectionString));
builder.Services.AddSingleton<ConfigManager>(); builder.Services.AddScoped<DealServices>();
builder.Services.AddScoped<CategoryServices>();
builder.Services.AddScoped<ProviderServices>();
builder.Services.AddScoped<TagServices>();
builder.Services.AddScoped<PasswordService>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<UserServices>();
builder.Services.AddScoped<MarketDealServices>();
builder.Services.AddScoped<DealVoteServices>();
builder.Services.AddSingleton<ApiKeyService>();
builder.Services.AddScoped<SwaggerUserService>();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUIConfiguration();
    app.MapScalarApiReference("/scalar");
}

/* app.UseMiddleware<ApiKeyMiddleware>();
 */
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
