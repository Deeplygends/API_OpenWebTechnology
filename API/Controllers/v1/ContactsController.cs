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
    public class ContactsController : BaseApiController
    {
        [HttpGet]
        [Route("contacts")]
        public async Task<IActionResult> GetAll()
        {
            var response = await Mediator.Send(new GetAllContactsQuery());
            return HttpResponseResult(response);
        }

        [HttpGet]
        [Route("contacts/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await Mediator.Send(new GetContactByIdQuery() {Id = id});
            return HttpResponseResult(response);
        }

        [HttpPost]
        [Route("contacts")]
        public async Task<IActionResult> Post(CreateContactCommand command)
        {
            var response = await Mediator.Send(command);
            return HttpResponseResult(response);
        }

        [HttpPut]
        [Route("contacts")]
        public async Task<IActionResult> Put(UpdateContactCommand command)
        {
            var response = await Mediator.Send(command);
            return HttpResponseResult(response);
        }
        [HttpDelete]
        [Route("contacts")]
        public async Task<IActionResult> Delete(DeleteContactCommand command)
        {
            var response = await Mediator.Send(command);
            return HttpResponseResult(response);
        }
    }
}
