using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.Contacts.Commands;
using Application.Features.Contacts.Commands.CreateContact;
using Application.Features.Contacts.Queries;
using Application.Features.Contacts.Queries.GetAllContacts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ContactController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllContactsQuery()));
        }

        [HttpGet("{id}")]

    public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetContactByIdQuery(){ Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateContactCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateContactCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteContactCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
