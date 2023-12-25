using DbContexts.DbContextHangFire;
using DbContexts.DbContextTrendTraderPro;
using Entities.CoinPriceHistories;
using Entities.Coins;
using Entities.TrackCoins;
using Entities.Users;
using FluentValidation;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Models.SwaggerExampleModels;
using Repositorys.CoinPriceHistoryRepository;
using Repositorys.CoinRepository;
using Repositorys.TrackCoinRepository;
using Repositorys.UserRepositorys;
using Serilog;
using Serilog.Events;
using Services.AuthServices;
using Services.CoinPriceHistoryServices;
using Services.CoinServices;
using Services.TrackCoinServices;
using Swashbuckle.AspNetCore.Filters;
using System.Security.Claims;
using System.Text;
using TrendTraderPro.Jobs;
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

builder.Services.AddHangfire(config =>
{
    config.UseSqlServerStorage(builder.Configuration["ConnectionStrings:ConnectionStringHangFire"]);
    RecurringJob.AddOrUpdate<JobSetCoins>("job-run-set-coins",job => job.SetCoinsExecute(), "0 0 * * *"); //Her g�n saat 00:00'da �al���r.
});
builder.Services.AddHangfireServer();

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
builder.Services.AddDbContext<HangFireDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:ConnectionStringHangFire"]);
});

builder.Services.AddHttpClient();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICoinRepository, CoinRepository>();
builder.Services.AddScoped<ICoinPriceHistoriesRepository, CoinPriceHistoriesRepository>();
builder.Services.AddScoped<ITrackCoinRepository, TrackCoinRepository>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICoinService, CoinService>();
builder.Services.AddScoped<ICoinPriceHistoryService, CoinPriceHistoryService>();
builder.Services.AddScoped<ITrackCoinService,TrackCoinService>();

builder.Services.AddAutoMapper(typeof(UserProfile));
builder.Services.AddAutoMapper(typeof(CoinProfile));
builder.Services.AddAutoMapper(typeof(CoinPriceHistoryProfile));
builder.Services.AddAutoMapper(typeof(TrackCoinProfile));

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
        var contextTrendTraderPro = serviceScope.ServiceProvider.GetRequiredService<TrendTraderProDbContext>();
        var contextHangFire = serviceScope.ServiceProvider.GetRequiredService<HangFireDbContext>(); 
        await contextTrendTraderPro.Database.MigrateAsync();
        await contextHangFire.Database.MigrateAsync();
        Log.Information("Database migrations successfully implemented");
    }
    catch (Exception ex)
    {
        Log.Error($"Database could not be create or migrations could not be implemented: {ex.Message} - InnerEx: {ex.InnerException?.Message}");
    }
}

app.UseHttpsRedirection();

app.UseHangfireDashboard("/hangfire");

app.UseAuthentication();
app.UseAuthorization();

app.UseAuthorization();

app.MapControllers();

app.Run();
