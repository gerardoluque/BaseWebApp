using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domain;
using API.Persistence;
using MediatR;
using API.Application.Core;

namespace API.Application.Procesos.Commands
{
    public class CreateProceso
    {
        public class Command : IRequest<Result<int>>
        {
            public string Descr { get; set; }
            public string Tipo { get; set; }
            public string Icono { get; set; }
        }

        public class Handler(AppDbContext context) : IRequestHandler<Command, Result<int>>
        {
            public async Task<Result<int>> Handle(Command request, CancellationToken cancellationToken)
            {
                var proceso = new Proceso
                {
                    Descr = request.Descr,
                    Tipo = request.Tipo,
                    Icono = request.Icono,
                    FechaCreacion = DateTime.UtcNow,
                    FechaUltimaActualizacion = DateTime.UtcNow
                };

                context.Procesos.Add(proceso);

                var result = await context.SaveChangesAsync(cancellationToken) > 0;

                if (!result)
                {
                    return Result<int>.Failure("Error al crear el proceso", 400);
                }

                return Result<int>.Success(proceso.Id);
            }
        }
    }

    public class DeleteProceso
    {
        public class Command : IRequest<Result<Unit>>
        {
            public int Id { get; set; }
        }

        public class Handler(AppDbContext context) : IRequestHandler<Command, Result<Unit>>
        {
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var proceso = await context
                    .Procesos
                    .FindAsync([request.Id], cancellationToken);

                if (proceso == null)
                {
                    return Result<Unit>.Failure("No se encontró el proceso", 404);
                }

                context.Procesos.Remove(proceso);
                
                var result = await context.SaveChangesAsync(cancellationToken) > 0;

                if (!result)
                {
                    return Result<Unit>.Failure("Error al eliminar el proceso", 400);
                }

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }

   public class UpdateProceso
    {
        public class Command : IRequest<Result<Unit>>
        {
            public int Id { get; set; }
            public string Descr { get; set; }
            public string Tipo { get; set; }
            public string Icono { get; set; }
        }

        public class Handler(AppDbContext context) : IRequestHandler<Command, Result<Unit>>
        {
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var proceso = await context.Procesos.FindAsync(new object[] { request.Id }, cancellationToken);

                if (proceso == null)
                {
                    return Result<Unit>.Failure("No se encontró el proceso", 404);
                }

                proceso.Descr = request.Descr;
                proceso.Tipo = request.Tipo;
                proceso.Icono = request.Icono;
                proceso.FechaUltimaActualizacion = DateTime.UtcNow;

                var result = await context.SaveChangesAsync(cancellationToken) > 0;

                if (!result)
                {
                    return Result<Unit>.Failure("Error al actualizar el proceso", 400);
                }

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }        
}