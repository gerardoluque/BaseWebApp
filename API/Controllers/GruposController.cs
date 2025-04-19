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
using Microsoft.Graph.Models;

namespace API.Controllers
{
    public class GruposController() : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<Group>>> GetGrupos()
        {
            return await Mediator.Send(new GetGrupoListAzureAD.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Group>> GetGrupo(string id)
        {
            return HandleResult(await Mediator.Send(new GetGrupoByIdAzureAD.Query { Id = id }));
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateGrupo([FromBody] CreateGrupoAzureAD.Command command)
        {
            return HandleResult(await Mediator.Send(command));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGrupo(string id, [FromBody] UpdateGrupoAzureAD.Command command)
        {
            command.Id = id;
            return HandleResult(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrupo(string id)
        {
            return HandleResult(await Mediator.Send(new DeleteGrupoAzureAD.Command { Id = id }));
        }        
    }
}