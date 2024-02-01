using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text.Json.Serialization;
using WorkoutAppApi.Auth;
using WorkoutAppApi.Data;
using WorkoutAppApi.Repositories;
using WorkoutAppApi.Repositories.Interfaces;
using WorkoutAppApi.Services;
using WorkoutAppApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Auth0
var domain = $"https://{builder.Configuration["Auth0:Domain"]}/";
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.Authority = domain;
    options.Audience = builder.Configuration["Auth0:Audience"];
    options.TokenValidationParameters = new TokenValidationParameters
    {
        NameClaimType = ClaimTypes.NameIdentifier
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("admin", policy => policy.Requirements.Add(new
    HasScopeRequirement("admin", domain)));
    options.AddPolicy("user", policy => policy.Requirements.Add(new
    HasScopeRequirement("user", domain)));
});

builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
}); 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddTransient<IExerciseService, ExerciseService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IExerciseRepository, ExerciseRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthorization();

app.MapControllers();

app.Run();
