using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Application.Grupos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using API.Domain;
using API.Application.Grupos.Queries;
using API.Application.Grupos.Commands;

namespace API.Controllers
{
    public class GruposController() : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<Grupo>>> GetGrupos()
        {
            return await Mediator.Send(new GetGrupoList.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Grupo>> GetGrupo(int id)
        {
            return HandleResult(await Mediator.Send(new GetGrupoDetails.Query { Id = id }));
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateGrupo([FromBody] CreateGrupo.Command command)
        {
            return HandleResult(await Mediator.Send(command));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGrupo(int id, [FromBody] UpdateGrupo.Command command)
        {
            command.Id = id;
            return HandleResult(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrupo(int id)
        {
            return HandleResult(await Mediator.Send(new DeleteGrupo.Command { Id = id }));
        }        
    }
}