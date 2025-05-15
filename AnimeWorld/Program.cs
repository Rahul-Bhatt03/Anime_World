using AnimeWorld.Data;
using AnimeWorld.Repositories;
using AnimeWorld.Interfaces;
//using AnimeWorld.Repositories;
using AnimeWorld.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using AnimeWorld.Entities;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AnimeWorld API", Version = "v1" });

    // Add this to handle duplicate class names
    c.CustomSchemaIds(x => x.FullName);

    // OR use this alternative for cleaner schema IDs
    // c.CustomSchemaIds(type => type.ToString().Replace("+", "."));
});

// Add this configuration before builder.Build()
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenLocalhost(7246, listenOptions =>
    {
        listenOptions.UseHttps();
    });
});



// Register Repositories
builder.Services.AddScoped<IAnimeRepository, AnimeRepository>();
builder.Services.AddScoped<ISeasonRepository, SeasonRepository>();
builder.Services.AddScoped<IEpisodeRepository, EpisodeRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Register Services
builder.Services.AddScoped<IAnimeService, AnimeService>();
builder.Services.AddScoped<ISeasonService, SeasonService>();
builder.Services.AddScoped<IEpisodeService, EpisodeService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddSingleton<IPasswordHasher<Users>, PasswordHasher<Users>>();

//once builder.build is used then after , the builder.service becomes read only and we cant use it further more so use it before builder.build
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactFrontend", policy => policy
        .WithOrigins("http://localhost:5173", "https://localhost:7246")  //here react frontend url is used 
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());  //only if we are using cookies or auth 
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AnimeWorld API v1"));
}

app.UseHttpsRedirection();
// Add this before app.UseAuthorization()
app.UseCors("AllowReactFrontend");
app.UseAuthorization();
app.MapControllers();

app.Run();