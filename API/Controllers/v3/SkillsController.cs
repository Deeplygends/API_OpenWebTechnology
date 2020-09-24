using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.Skill.Commands;
using Application.Features.Skill.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v3
{
    [ApiVersion("3.0")]
    [Authorize]
    public class SkillsController : BaseApiController
    {
        /// <summary>
        /// Get All Skills 
        /// </summary>
        /// <remarks>
        /// Sample Request :
        ///     GET api/v2/skills
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">Return the list of skills</response>
        [HttpGet]
        [ProducesResponseType((200))]
        public async Task<IActionResult> GetAll()
        {
            var response = await Mediator.Send(new GetAllSkillsQuery());
            return HttpResponseResult(response);
        }
        /// <summary>
        /// Get a skill by id
        /// </summary>
        /// <remarks>
        /// Sample Request :
        ///     GET api/v2/skills/1
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">Return the list of skills</response>
        /// <response code="400">Invalid id provide</response>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await Mediator.Send(new GetSkillByIdQuery() {Id = id});
            return HttpResponseResult(response);
        }
        /// <summary>
        /// Get All Levels
        /// </summary>
        /// <remarks>
        /// Sample Request :
        ///     GET api/v2/skills/levels
        /// </remarks>
        /// <response code="200">Return the list of all levels</response>
        /// <returns></returns>
        [HttpGet]
        [Route("levels")]
        public async Task<IActionResult> GetAllLevels()
        {
            var response = await Mediator.Send(new GetAllSkillLevelQuery());
            return HttpResponseResult(response);
        }
        /// <summary>
        /// Create a Skill
        /// </summary>
        /// <remarks>
        /// Sample Request :
        ///     POST api/v2/skills
        ///     {
        ///         "Name" : "Java",
        ///         "Level" : "Expert"
        ///     }
        ///
        /// The Level must come from the Levels List
        /// </remarks>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <response code="200">The ressource has been created</response>
        /// <response code="400">The level is not a valid level</response>
        /// <response code="409">the ressource already exists (Name/Level unique)</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> Post(CreateSkillCommand command)
        {
            var response = await Mediator.Send(command);
            return HttpResponseResult(response);
        }
        /// <summary>
        /// Update a Skill
        /// </summary>
        /// <remarks>
        /// Sample request :
        ///     PUT api/v2/skills
        ///     {
        ///         "Id" : 1,
        ///         "Name" : "New Name",
        ///         "Level" : "Intermediate"
        ///     }
        /// </remarks>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <response code="204">The ressource has been updated</response>
        /// <response code="400">The level is not a valid level or the ressource already exists (Name/Level unique)</response>
        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Put(UpdateSkillCommand command)
        {
            var response = await Mediator.Send(command);
            return HttpResponseResult(response);
        }
        /// <summary>
        /// Delete a Skill
        /// </summary>
        /// <remarks>
        /// Sample request :
        ///     DELETE api/v2/skills
        ///     {
        ///         "Id" : 1
        ///     }
        /// </remarks>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <response code="200">The ressource has been deleted</response>
        /// <response code="400">The id is not valid</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteSkillCommand command)
        {
            var response = await Mediator.Send(command);
            return HttpResponseResult(response);
        }

    }
}
