using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using API.Domain;
using API.Persistence;
using MediatR;
using API.Application.Core;
using AutoMapper;

namespace API.Application.Grupos.Commands
{
    public class CreateGrupo
    {
        public class Command : IRequest<Result<int>>
        {
            public string Descr { get; set; }
        }

        public class Handler(AppDbContext context) : IRequestHandler<Command, Result<int>>
        {
            public async Task<Result<int>> Handle(Command request, CancellationToken cancellationToken)
            {
                var grupo = new Grupo
                {
                    Descr = request.Descr,
                    FechaCreacion = DateTime.UtcNow,
                    FechaUltimaActualizacion = DateTime.UtcNow
                };

                context.Grupos.Add(grupo);

                var result = await context.SaveChangesAsync(cancellationToken) > 0;

                if (!result)
                {
                    return Result<int>.Failure("Error al crear el grupo", 400);
                }

                return Result<int>.Success(grupo.Id);
            }
        }
    }

    public class DeleteGrupo
    {
        public class Command : IRequest<Result<Unit>>
        {
            public int Id { get; set; }
        }

        public class Handler(AppDbContext context) : IRequestHandler<Command, Result<Unit>>
        {
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var grupo = await context.Grupos.FindAsync(new object[] { request.Id }, cancellationToken);

                if (grupo == null)
                {
                    return Result<Unit>.Failure("No se encontró el grupo", 404);
                }

                context.Grupos.Remove(grupo);

                var result = await context.SaveChangesAsync(cancellationToken) > 0;

                if (!result)
                {
                    return Result<Unit>.Failure("Error al eliminar el grupo", 400);
                }

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }

    public class UpdateGrupo
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
                var grupo = await context.Grupos.FindAsync(new object[] { request.Id }, cancellationToken);

                if (grupo == null)
                {
                    return Result<Unit>.Failure("No se encontró el grupo", 404);
                }

                mapper.Map(request, grupo); 

                // grupo.Descr = request.Descr;
                // grupo.FechaUltimaActualizacion = DateTime.UtcNow;

                var result = await context.SaveChangesAsync(cancellationToken) > 0;

                if (!result)
                {
                    return Result<Unit>.Failure("Error al actualizar el grupo", 400);
                }

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}