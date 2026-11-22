using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Bll.Service;
using DLL.Context;
using DLL.Repository;
using Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UI.MapperProfile;
using Bll.MapperProfile;
using UI.Vlaidators;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactClient", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Components ??= new OpenApiComponents();

        document.Components.SecuritySchemes = new Dictionary<string, IOpenApiSecurityScheme>
        {
            ["Bearer"] = new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Description = "Enter JWT token"
            }
        };

        foreach (var operation in document.Paths.Values
                     .SelectMany(path => path.Operations))
        {
            operation.Value.Security ??= [];

            operation.Value.Security.Add(new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference("Bearer", document)] = []
            });
        }

        return Task.CompletedTask;
    });
});

// Додаємо контекст бази даних та налаштовуємо підключення до SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Додаємо Identity та налаштовуємо параметри пароля 
builder.Services.AddIdentityCore<User>(options =>
{
    //Password settings
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;

    //User settings
    options.Lockout.AllowedForNewUsers = true;
    options.SignIn.RequireConfirmedEmail = false;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.AllowedForNewUsers = true;

})
.AddSignInManager()
.AddEntityFrameworkStores<AppDbContext>();

// Додаємо аутентифікацію з використанням JWT
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.MapInboundClaims = false; //Вимикаємо автоматичне відображення стандартних JWT-клеймів на клейми .NET
                                          // Налаштовуємо параметри валідації токенів
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true, // Перевіряємо видавця токена
            ValidateAudience = true, // Перевіряємо аудиторію токена
            ValidateLifetime = true, // Перевіряємо термін дії токена
            ValidateIssuerSigningKey = true, // Перевіряємо підпис токена
            ValidIssuer = builder.Configuration["JWT:issuer"], // Встановлюємо допустимого видавця токена
            ValidAudience = builder.Configuration["JWT:audience"], //Встановлюємо допустиму аудиторію токена
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:key"])),
            ClockSkew = TimeSpan.Zero,

            NameClaimType = JwtRegisteredClaimNames.UniqueName,
        };
    });

//Додаємо авторизацію
builder.Services.AddAuthorization();

//builder.Services.AddTransient<UserService>();

builder.Services.AddValidatorsFromAssemblyContaining<IdentityRegisterRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<IdentityLoginRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<MovieValidator>();

//builder.Services.AddTransient<UserService>();

builder.Services.AddScoped<IdentityAuthService>();
builder.Services.AddScoped<JwtTokenService>();

builder.Services.AddScoped<MovieService>();
builder.Services.AddScoped<MovieRepository>();

builder.Services.AddAutoMapper(opt =>
{
    opt.AddProfile<RequestResponseProfile>();
    opt.AddProfile<DtoMapperProfile>();
    opt.AddProfile<MovieProfile>();
});

var app = builder.Build();
app.MapOpenApi();



app.UseSwaggerUI(o => o.SwaggerEndpoint("/openapi/v1.json", "EFDiExample"));

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}

app.UseHttpsRedirection();

app.UseCors("ReactClient");

app.UseAuthentication(); // 1 додаємо middleware для аутентифікації (перевірка пароля) 
app.UseAuthorization(); // 2 додаємо middleware для авторизації (перевірка прав доступу)

app.MapControllers();

app.Run();
