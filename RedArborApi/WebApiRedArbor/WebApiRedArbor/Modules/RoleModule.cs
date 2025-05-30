using System.Net;
using WebApiRedArbor.Entities;
using WebApiRedArbor.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApiRedArbor.Modules
{
    public static class RoleModule
    {
        public static void MapRoleEndPoints(this IEndpointRouteBuilder routes) 
        {
            var group = routes.MapGroup("/Role");

            group.MapGet("/List", async (IServiceRole service) =>
            {
                var list = await service.ListAsync();
                return (list != null && list.Any()) ? Results.Ok(list) : Results.NotFound("No hay elementos en esta lista");
            })
            .Produces<IEnumerable<Role>>(StatusCodes.Status200OK)  
            .Produces<string>(StatusCodes.Status404NotFound)        
            .Produces<string>(StatusCodes.Status500InternalServerError)
            .WithMetadata(new SwaggerOperationAttribute("Obtiene la lista de roles"));


            group.MapPost("/Add", async (IServiceRole service, Role entidad) =>
            {
                var model = await service.AddAsync(entidad);
                return (model != null) ? Results.Ok(model) : Results.NotFound("No hay elementos en esta lista");
            })
            .Produces<Role>(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status404NotFound)
            .Produces<string>(StatusCodes.Status500InternalServerError)
            .WithMetadata(new SwaggerOperationAttribute("Agrega infoprmación en al tabla roles"));

        }
    }
}
