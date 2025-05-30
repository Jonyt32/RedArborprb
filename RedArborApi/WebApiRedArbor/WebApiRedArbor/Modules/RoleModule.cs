
using WebApiRedArbor.Entities;
using WebApiRedArbor.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApiRedArbor.Modules
{
    public static class RoleModule
    {
        public static void MapRoleEndPoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/Role");

            // Obtener lista de roles
            group.MapGet("/List", async (IServiceRole service) =>
            {
                var list = await service.ListAsync();
                return (list != null && list.Any()) ? Results.Ok(list) : Results.NotFound("No hay elementos en esta lista");
            })
            .Produces<IEnumerable<Role>>(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status404NotFound)
            .Produces<string>(StatusCodes.Status500InternalServerError)
            .WithOpenApi();

            // Agregar un nuevo rol
            group.MapPost("/Add", async (IServiceRole service, [FromBody] Role entidad) =>
            {
                var model = await service.AddAsync(entidad);
                return (model != null) ? Results.Ok(model) : Results.NotFound("No se pudo agregar el rol");
            })
            .Produces<Role>(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status404NotFound)
            .Produces<string>(StatusCodes.Status500InternalServerError)
            .WithOpenApi();
        }
    }
}
