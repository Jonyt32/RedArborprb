using Microsoft.EntityFrameworkCore;
using WebApiRedArbor.Context;
using WebApiRedArbor.Extensiones;
using WebApiRedArbor.Modules;

var builder = WebApplication.CreateBuilder(args);
//Variables para validar el rango de fecha
var minAge = builder.Configuration["AgeLimits:MinAge"];
var maxAge = builder.Configuration["AgeLimits:MaxAge"];
Environment.SetEnvironmentVariable("MIN_AGE", minAge);
Environment.SetEnvironmentVariable("MAX_AGE", maxAge);


// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("Data Source=employees.db"));
builder.Services.AddInternalDependencies(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
// Map the endpoints
app.MapRoleEndPoints();
app.MapEmployeeEndPoints();

app.Run();
