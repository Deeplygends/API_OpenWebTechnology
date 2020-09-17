﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.Skill.Commands;
using Application.Features.Skill.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1
{
    [ApiVersion("1.0")]
    public class SkillsController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await Mediator.Send(new GetAllSkillsQuery());
            return HttpResponseResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateSkillCommand command)
        {
            var response = await Mediator.Send(command);
            return HttpResponseResult(response);
        }

    }
}
