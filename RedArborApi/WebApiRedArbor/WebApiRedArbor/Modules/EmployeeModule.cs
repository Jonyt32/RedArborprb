using Swashbuckle.AspNetCore.Annotations;
using WebApiRedArbor.Entities;
using WebApiRedArbor.Interfaces;

namespace WebApiRedArbor.Modules
{
    public static class EmployeeModule
    {
        public static void MapEmployeeEndPoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/Employee");

            group.MapGet("/List", async (IServiceEmployee service) =>
            {
                var list = await service.ListAsync();
                return (list != null && list.Any()) ? Results.Ok(list) : Results.NotFound("No hay elementos en esta lista");
            })
            .Produces<IEnumerable<Employee>>(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status404NotFound)
            .Produces<string>(StatusCodes.Status500InternalServerError)
            .WithMetadata(new SwaggerOperationAttribute("Obtiene la lista de Employees"));

            group.MapGet("/GetId", async (IServiceEmployee service, int employeeId) =>
            {
                var employee = await service.GetIdAsync(employeeId);
                return (employee != null) ? Results.Ok(employee) : Results.NotFound("No hay elementos en esta lista");
            })
            .Produces<Employee>(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status404NotFound)
            .Produces<string>(StatusCodes.Status500InternalServerError)
            .WithMetadata(new SwaggerOperationAttribute("Obtiene el employee por Id"));

            group.MapPost("/Add", async (IServiceEmployee service, Employee entidad) =>
            {
                var model = await service.AddAsync(entidad);
                return (model != null) ? Results.Ok(model) : Results.NotFound("No hay elementos en esta lista");
            })
            .Produces<Role>(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status404NotFound)
            .Produces<string>(StatusCodes.Status500InternalServerError)
            .WithMetadata(new SwaggerOperationAttribute("Agrega un employee"));

            group.MapPut("/Update", async (IServiceEmployee service, Employee entidad) =>
            {
                await service.UpdateAsync(entidad);
                return  Results.Ok("Actualizo con éxito");
            })
            .Produces<string>(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status404NotFound)
            .Produces<string>(StatusCodes.Status500InternalServerError)
            .WithMetadata(new SwaggerOperationAttribute("Agrega un employee"));

            group.MapDelete("/Delete", async (IServiceEmployee service, int employeeId) =>
            {
                await service.DeleteAsync(employeeId);
                return Results.Ok("Elimino con éxito");
            })
            .Produces<string>(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status404NotFound)
            .Produces<string>(StatusCodes.Status500InternalServerError)
            .WithMetadata(new SwaggerOperationAttribute("Agrega un employee"));

        }
    }
}
