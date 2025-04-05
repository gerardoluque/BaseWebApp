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
    }
}