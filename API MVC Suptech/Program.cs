using API_MVC_Suptech.Data;
using API_MVC_Suptech.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
<<<<<<< HEAD
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CrudData>(options =>
options.UseSqlServer(connectionString));
=======
var conn = builder.Configuration["ConnectionStrings:default"];
builder.Services.AddDbContext<CrudData>(x => x.UseSqlServer(conn));
>>>>>>> 603368ed9cbff29bdae0d687f4b4b13c00635cdb
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
}

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API MVC Suptech v1");
    c.RoutePrefix = "swagger"; // URL = /swagger
});

app.UseCors("policy");

app.UseAuthorization();

app.MapControllers();

app.Run();


