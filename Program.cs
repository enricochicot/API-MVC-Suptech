using API_MVC_Suptech.Data;
using API_MVC_Suptech.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? Environment.GetEnvironmentVariable("DATABASE_URL");

builder.Services.AddDbContext<CrudData>(options =>
    options.UseSqlServer(connectionString)
);

// Configuração de Autenticação JWT
var jwtKey = builder.Configuration["Jwt:Key"];
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };

    // Configuração de eventos para logging
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
            logger.LogWarning("Falha na autenticação: {Error} | Endpoint: {Path}",
                context.Exception.Message,
                context.HttpContext.Request.Path);
            return Task.CompletedTask;
        },
        OnChallenge = context =>
        {
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
            logger.LogWarning("Acesso não autorizado (sem token ou token inválido) | Endpoint: {Path} | IP: {IP}",
                context.Request.Path,
                context.HttpContext.Connection.RemoteIpAddress);
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
            var userName = context.Principal?.Identity?.Name ?? "Desconhecido";
            logger.LogInformation("Token validado com sucesso | Usuário: {User} | Endpoint: {Path}",
                userName,
                context.HttpContext.Request.Path);
            return Task.CompletedTask;
        },
        OnForbidden = context =>
        {
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
            logger.LogWarning("Acesso negado (sem permissão) | Endpoint: {Path} | IP: {IP}",
                context.Request.Path,
                context.HttpContext.Connection.RemoteIpAddress);
            return Task.CompletedTask;
        }
    };

    });

builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "API MVC Suptech",
            Version = "v1"
        });

        // Adiciona suporte a Bearer Token no Swagger
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header. Exemplo: \"Bearer {token}\"",
            Name = "Authorization",
            In = ParameterLocation.Header,
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
    }
);


builder.Services.AddScoped<TokenService>();
builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: "policy", policy => { policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod(); });
    }
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API MVC Suptech v1");
    c.RoutePrefix = "swagger"; // URL = /swagger
});

app.UseCors("policy");

app.UseAuthentication(); // Adiciona autenticação
app.UseAuthorization();

app.MapControllers();

app.Run();


// Usa validação nativa do ASP.NET Core
// Funciona com Swagger automaticamente

// 4 eventos principais para logging:
// OnAuthenticationFailed: Quando a autenticação falha (token inválido, expirado, etc)
// OnChallenge: Quando uma requisição não autenticada tenta acessar um endpoint protegido
// OnTokenValidated: Quando o token é validado com sucesso
// OnForbidden: Quando um usuário autenticado tenta acessar um recurso sem permissão