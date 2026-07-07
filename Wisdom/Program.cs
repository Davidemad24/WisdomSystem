using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Wisdom.Configurations;
using Wisdom.Persistence;
using Wisdom.Repositories.Implementation;
using Wisdom.Repositories.Interfaces;
using Wisdom.Services.Implementation;
using Wisdom.Services.Interfaces;

// Create web application builder
var builder = WebApplication.CreateBuilder(args);

// Register AppDbContext
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("SqlServer")
));

// Register memory cache
builder.Services.AddMemoryCache();

// Register jwt and add authentication
var jwtConfiguration = new JwtConfiguration();
builder.Configuration.GetSection("JWT").Bind(jwtConfiguration);
builder.Services.AddSingleton(jwtConfiguration);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = jwtConfiguration.Issuer,
        ValidAudience = jwtConfiguration.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.SigningKey))
    };
});

// Register auto mapper
builder.Services.AddAutoMapper(config => {}, Assembly.GetExecutingAssembly());

// Register repositories
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IRefreshTokenRepo, RefreshTokenRepo>();
builder.Services.AddScoped<IWisdomRepo, WisdomRepo>();

// Register services
builder.Services.AddScoped<ICacheServices, CacheServices>();
builder.Services.AddScoped<IJwtServices, JwtServices>();
builder.Services.AddScoped<IRefreshTokenServices, RefreshTokenServices>();
builder.Services.AddScoped<IAuthenticationServices, AuthenticationServices>();
builder.Services.AddScoped<IWisdomServices, WisdomServices>();

// Add controllers
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Create web application
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Http redirection, authorization
app.UseHttpsRedirection();
app.UseAuthorization();

// Map controllers
app.MapControllers();

// Run application
app.Run();