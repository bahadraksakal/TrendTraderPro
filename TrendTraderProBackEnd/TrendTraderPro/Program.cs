using DbContexts.DbContextTrendTraderPro;
using Entities.Coins;
using Entities.Users;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Models.SwaggerExampleModels;
using Repositorys.CoinRepository;
using Repositorys.UserRepositorys;
using Serilog;
using Serilog.Events;
using Services.AuthServices;
using Services.GeckoApiServices;
using Swashbuckle.AspNetCore.Filters;
using System.Security.Claims;
using System.Text;
using Validators.FluentValidators.UserValidators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSerilog();
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    //.Enrich.FromLogContext()
    .WriteTo.Console().MinimumLevel.Warning()
    .WriteTo.Debug().MinimumLevel.Warning()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day).MinimumLevel.Debug()
    .CreateLogger();

builder.Services.AddSwaggerExamplesFromAssemblyOf<LoginExampleModel>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<RegisterExampleModel>();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
    c.EnableAnnotations();
    c.ExampleFilters();
});

builder.Services.AddDbContext<TrendTraderProDbContext>(options => {
    options.UseSqlServer(builder.Configuration["ConnectionStrings:ConnectionString"]);
});

builder.Services.AddHttpClient();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICoinRepository, CoinRepository>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICoinService, CoinService>();

builder.Services.AddAutoMapper(typeof(UserProfile));
builder.Services.AddAutoMapper(typeof(CoinProfile));

//builder.Services.AddFluentValidationAutoValidation();
//builder.Services.AddValidatorsFromAssemblyContaining<UserRegisterValidator>();
builder.Services.AddScoped<IValidator<UserDTO>, UserDTOValidator>();

var issuer = builder.Configuration["JwtConfig:Issuer"];
var audience = builder.Configuration["JwtConfig:Audience"];
var signingKey = builder.Configuration["JwtConfig:SigningKey"] ?? "";
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey)),
    };
});
builder.Services.AddAuthorization();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CustomAdminPolicy", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim(ClaimTypes.Role, "Admin");

    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var serviceScope = app.Services.CreateScope())
{
    try
    {
        var context = serviceScope.ServiceProvider.GetRequiredService<TrendTraderProDbContext>();
        await context.Database.MigrateAsync();
    }
    catch (Exception ex)
    {
        Log.Error("Veri tabaný güncellenemedi veya baþarýyla oluþturulamadý. Mesaj: " + ex.Message);
    }
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseAuthorization();

app.MapControllers();

app.Run();
