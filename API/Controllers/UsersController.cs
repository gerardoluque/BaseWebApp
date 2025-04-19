using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.Models;
using API.Domain;
using MediatR;
using API.Application.Usuarios.Queries;
using API.Application.Usuarios.Commands;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            return await Mediator.Send(new GetUserListAzureAD.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            return HandleResult(await Mediator.Send(new GetUserByIdAzureAD.Query { Id = id }));
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateUser([FromBody] CreateUserAzureAD.Command command)
        {
            return HandleResult(await Mediator.Send(command));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserAzureAD.Command command)
        {
            command.Id = id;
            return HandleResult(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            return HandleResult(await Mediator.Send(new DeleteUserAzureAD.Command { Id = id }));
        }
    }
}
