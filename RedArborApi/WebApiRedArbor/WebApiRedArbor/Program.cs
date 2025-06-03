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
builder.Services.AddSwaggerGen(options =>
{
    // Configura la versión OpenAPI
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "WebApiRedArbor",
        Version = "v1",
        Description = "Descripción WebApiRedArbor de la API"
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()   
               .AllowAnyMethod()   
               .AllowAnyHeader();  
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.EnsureCreated();  
    dbContext.SeedRoles();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiRedArbor v1"); 
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");
app.UseAuthorization();

app.MapControllers();
// Map the endpoints
app.MapRoleEndPoints();
app.MapEmployeeEndPoints();

app.Run();
