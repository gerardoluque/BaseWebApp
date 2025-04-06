using System;
using System.Threading;
using System.Threading.Tasks;
using API.Domain;
using API.Persistence;
using API.Application.Core;
using MediatR;
using AutoMapper;

namespace API.Application.Roles.Commands
{
    public class CreateRol
    {
        public class Command : IRequest<Result<int>>
        {
            public string Descr { get; set; }
        }

        public class Handler(AppDbContext context) : IRequestHandler<Command, Result<int>>
        {
            public async Task<Result<int>> Handle(Command request, CancellationToken cancellationToken)
            {
                var rol = new Rol
                {
                    Descr = request.Descr,
                    FechaCreacion = DateTime.UtcNow,
                    FechaUltimaActualizacion = DateTime.UtcNow
                };

                context.Roles.Add(rol);

                var result = await context.SaveChangesAsync(cancellationToken) > 0;

                if (!result)
                {
                    return Result<int>.Failure("Error al crear el rol", 400);
                }

                return Result<int>.Success(rol.Id);
            }
        }
    }

    public class UpdateRol
    {
        public class Command : IRequest<Result<Unit>>
        {
            public int Id { get; set; }
            public string Descr { get; set; }
        }

        public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command, Result<Unit>>
        {
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var rol = await context.Roles.FindAsync(new object[] { request.Id }, cancellationToken);

                if (rol == null)
                {
                    return Result<Unit>.Failure("Rol no encontrado", 404);
                }

                mapper.Map(request, rol); 

                // rol.Descr = request.Descr;
                // rol.FechaUltimaActualizacion = DateTime.UtcNow;

                var result = await context.SaveChangesAsync(cancellationToken) > 0;

                if (!result)
                {
                    return Result<Unit>.Failure("Error al actualizar el rol", 400);
                }

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }

    public class DeleteRol
    {
        public class Command : IRequest<Result<Unit>>
        {
            public int Id { get; set; }
        }

        public class Handler(AppDbContext context) : IRequestHandler<Command, Result<Unit>>
        {
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var rol = await context.Roles.FindAsync(new object[] { request.Id }, cancellationToken);

                if (rol == null)
                {
                    return Result<Unit>.Failure("Rol no encontrado", 404);
                }

                context.Roles.Remove(rol);

                var result = await context.SaveChangesAsync(cancellationToken) > 0;

                if (!result)
                {
                    return Result<Unit>.Failure("Error al eliminar el rol", 400);
                }

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}