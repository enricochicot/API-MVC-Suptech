using API_MVC_Suptech.Data;
using API_MVC_Suptech.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? Environment.GetEnvironmentVariable("DATABASE_URL");

builder.Services.AddDbContext<CrudData>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API MVC Suptech",
        Version = "v1"
    });
});
builder.Services.AddScoped<TokenService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "policy", policy => { policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod(); });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API MVC Suptech v1");
        c.RoutePrefix = "swagger"; // URL = /swagger
    });
}
else
{
    // Em produção, ainda queremos o Swagger disponível
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API MVC Suptech v1");
        c.RoutePrefix = "swagger"; // URL = /swagger
    });
}

// Para Vercel, remover o UseHttpsRedirection em produção
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseCors("policy");

app.UseAuthorization();

app.MapControllers();

app.Run();


