using System.Collections.Generic;
using System.Threading.Tasks;
using API.Application.Roles.Commands;
using API.Application.Roles.Queries;
using API.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<Rol>>> GetRoles()
        {
            return await Mediator.Send(new GetRolList.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Rol>> GetRol(int id)
        {
            return HandleResult(await Mediator.Send(new GetRolById.Query { Id = id }));
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateRol([FromBody] CreateRol.Command command)
        {
            return HandleResult(await Mediator.Send(command));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRol(int id, [FromBody] UpdateRol.Command command)
        {
            command.Id = id;
            return HandleResult(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRol(int id)
        {
            return HandleResult(await Mediator.Send(new DeleteRol.Command { Id = id }));
        }
    }
}