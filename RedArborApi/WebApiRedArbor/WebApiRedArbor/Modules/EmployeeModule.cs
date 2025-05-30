using Microsoft.AspNetCore.Mvc;
using WebApiRedArbor.Entities;
using WebApiRedArbor.Interfaces;

namespace WebApiRedArbor.Modules
{
    public static class EmployeeModule
    {
        public static void MapEmployeeEndPoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/Employee");

            // Obtener lista de empleados
            group.MapGet("/List", async (IServiceEmployee service) =>
            {
                var list = await service.ListAsync();
                return (list != null && list.Any()) ? Results.Ok(list) : Results.NotFound("No hay elementos en esta lista");
            })
            .Produces<IEnumerable<Employee>>(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status404NotFound)
            .Produces<string>(StatusCodes.Status500InternalServerError)
            .WithOpenApi();

            // Obtener empleado por ID
            group.MapGet("/GetId", async (IServiceEmployee service, [FromQuery] int employeeId) =>
            {
                var employee = await service.GetIdAsync(employeeId);
                return (employee != null) ? Results.Ok(employee) : Results.NotFound("Empleado no encontrado");
            })
            .Produces<Employee>(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status404NotFound)
            .Produces<string>(StatusCodes.Status500InternalServerError)
            .WithOpenApi();

            // Agregar un nuevo empleado
            group.MapPost("/Add", async (IServiceEmployee service, [FromBody] Employee entidad) =>
            {
                var model = await service.AddAsync(entidad);
                return (model != null) ? Results.Ok(model) : Results.NotFound("No se pudo agregar el empleado");
            })
            .Produces<Employee>(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status404NotFound)
            .Produces<string>(StatusCodes.Status500InternalServerError)
            .WithOpenApi();

            // Actualizar empleado
            group.MapPut("/Update", async (IServiceEmployee service, [FromBody] Employee entidad) =>
            {
                await service.UpdateAsync(entidad);
                return Results.Ok("Empleado actualizado con éxito");
            })
            .Produces<string>(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status404NotFound)
            .Produces<string>(StatusCodes.Status500InternalServerError)
            .WithOpenApi();

            // Eliminar empleado
            group.MapDelete("/Delete", async (IServiceEmployee service, [FromQuery] int employeeId) =>
            {
                await service.DeleteAsync(employeeId);
                return Results.Ok("Empleado eliminado con éxito");
            })
            .Produces<string>(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status404NotFound)
            .Produces<string>(StatusCodes.Status500InternalServerError)
            .WithOpenApi();
        }
    }
}
