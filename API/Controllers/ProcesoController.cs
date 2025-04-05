using System.Threading.Tasks;
using API.Application.Procesos.Commands;
using API.Application.Procesos.Queries;
using API.Domain;
using Microsoft.AspNetCore.Mvc;
using API.Application.Core;
using MediatR;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProcesoController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<Proceso>>> GetProcesos()
        {
            return await Mediator.Send(new GetProcesoList.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Proceso>> GetProceso(int id)
        {
            return HandleResult(await Mediator.Send(new GetProcesoDetails.Query { Id = id }));
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateProceso([FromBody] CreateProceso.Command command)
        {
            return HandleResult(await Mediator.Send(command));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProceso(int id, [FromBody] UpdateProceso.Command command)
        {
            command.Id = id;
            return HandleResult(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProceso(int id)
        {
            return HandleResult(await Mediator.Send(new DeleteProceso.Command { Id = id }));
        }
    }
}