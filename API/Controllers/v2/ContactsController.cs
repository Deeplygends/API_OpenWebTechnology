using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Features.Contacts.Commands;
using Application.Features.Contacts.Commands.CreateContact;
using Application.Features.Contacts.Queries;
using Application.Features.Contacts.Queries.GetAllContacts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v2
{
    [ApiVersion("2.0")]
    public class ContactsController : BaseApiController
    {
        #region CRUD Contact
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await Mediator.Send(new GetAllContactsQuery());
            return HttpResponseResult(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await Mediator.Send(new GetContactByIdQuery() {Id = id});
            return HttpResponseResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateContactCommand command)
        {
            var response = await Mediator.Send(command);
            return HttpResponseResult(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateContactCommand command)
        {
            var response = await Mediator.Send(command);
            return HttpResponseResult(response);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteContactCommand command)
        {
            var response = await Mediator.Send(command);
            return HttpResponseResult(response);
        }
        #endregion
        #region CRUD Contact's Skill
        [HttpGet]
        [Route("{id}/skills")]
        public async Task<IActionResult> GetSkills(int id)
        {
            var response = await Mediator.Send(new GetSkillsOfContactQuery() {Id = id});
            return HttpResponseResult(response);
        }

        [HttpPost]
        [Route("{id}/skills")]
        public async Task<IActionResult> AddSkillToContact(int id, [FromBody]SkillDto skill)
        {
            var command = new AddSkillToContactCommand()
            {
                IdContact = id,
                Skill = skill
            };

            var response = await Mediator.Send(command);
            return HttpResponseResult(response);
        }
        #endregion
    }
}
