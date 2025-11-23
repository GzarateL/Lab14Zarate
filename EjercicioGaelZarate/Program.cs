using EjercicioGaelZarate.Application;
using EjercicioGaelZarate.Infrastructure;
using EjercicioGaelZarate.Middleware; // Importar el Middleware
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// --- 1. Registrar Servicios de las otras capas ---
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(configuration);

// --- 2. Registrar Servicios de la API ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// --- 3. Configurar Swagger para que use JWT ---
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Ticketero API", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Por favor ingrese el token JWT con 'Bearer ' al inicio",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new string[] {}
    }});
});

// --- 4. Configurar Autenticación JWT ---
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
        };
    });

var app = builder.Build();

// --- 5. Configurar el Pipeline de HTTP ---
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// --- 6. Añadir el Middleware de Errores ---
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

// --- 7. Añadir Autenticación y Autorización ---
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();