using System.Threading.Tasks;
using API.Application.Procesos.Commands;
using API.Application.Procesos.Queries;
using API.Domain;
using Microsoft.AspNetCore.Mvc;

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
            return await Mediator.Send(new GetProcesoDetails.Query { Id = id });
        }

        [HttpPost]
        public async Task<IActionResult> CreateProceso([FromBody] CreateProceso.Command command)
        {
            await Mediator.Send(command);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProceso(int id, [FromBody] UpdateProceso.Command command)
        {
            command.Id = id;
            await Mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProceso(int id)
        {
            await Mediator.Send(new DeleteProceso.Command { Id = id });
            return Ok();
        }
    }
}