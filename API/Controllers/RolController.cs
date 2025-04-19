using System.Collections.Generic;
using System.Threading.Tasks;
using API.Application.Roles.Commands;
using API.Application.Roles.Queries;
using API.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.Models;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<DirectoryRole>>> GetRoles()
        {
            return await Mediator.Send(new GetRolListAzureAD.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Rol>> GetRol(string id)
        {
            return HandleResult(await Mediator.Send(new GetRolByIdAzureAD.Query { Id = id }));
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateRol([FromBody] CreateRolAzureAD.Command command)
        {
            return HandleResult(await Mediator.Send(command));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRol(string id, [FromBody] UpdateRolAzureAD.Command command)
        {
            command.Id = id;
            return HandleResult(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRol(string id)
        {
            return HandleResult(await Mediator.Send(new DeleteRolAzureAD.Command { Id = id }));
        }
    }
}