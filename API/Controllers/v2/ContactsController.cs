using System.Threading.Tasks;
using Application.DTOs;
using Application.Features.Contacts.Commands;
using Application.Features.Contacts.Queries;
using Application.Features.Contacts.Queries.GetAllContacts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v2
{
    [ApiVersion("2.0")]
    public class ContactsController : BaseApiController
    {
        #region CRUD Contact
        /// <summary>
        /// Get All Contacts
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     GET api/v2/contacts
        /// </remarks>
        ///  <response code="200">Return the list of all contacts</response>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAll()
        {
            var response = await Mediator.Send(new GetAllContactsQuery());
            return HttpResponseResult(response);
        }

        /// <summary>
        /// Get a contact by id
        /// </summary>
        /// <remarks>
        ///Sample request:
        ///     GET api/v2/contacts/1
        /// </remarks>
        /// <param name="id">the id of the contact</param>
        /// <response code="200">Return the request contact</response>
        /// <response code="404">The contact id doesn't exists</response>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(int id)
        {
            var response = await Mediator.Send(new GetContactByIdQuery() {Id = id});
            return HttpResponseResult(response);
        }

        /// <summary>
        /// Create a new Contact
        /// </summary>
        /// <remarks>
        ///Sample request:
        /// Sample Request :
        ///     POST api/v2/contacts/
        ///     {
        ///         "FirstName" : "Mickael",
        ///         "LastName" : "Peeren",
        ///         "Email" : "test@test.fr",
        ///         "PhoneNumber" : "+33606060606",
        ///         "Address" : "6 rue de la liberté 78000 Versailles"
        ///     }
        /// </remarks>
        /// <param name="command">bind the body above into the command object</param>
        /// <returns></returns>
        /// <response code="200">The ressource is created</response>
        /// <response code="400">one or more parameters is(are) not valid</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post(CreateContactCommand command)
        {
            var response = await Mediator.Send(command);
            return HttpResponseResult(response);
        }

        /// <summary>
        /// Update a contact
        /// </summary>
        /// <remarks>
        /// Sample Request :
        ///     PUT api/v2/contacts/
        ///     {
        ///         "Id" : 1,
        ///         "FirstName" : "Mickael",
        ///         "LastName" : "Peeren",
        ///         "Email" : "test@test.fr",
        ///         "PhoneNumber" : "+33606060606",
        ///         "Address" : "6 rue de la liberté 78000 Versailles"
        ///     }
        /// </remarks>
        /// <param name="command">bind the body above into the command object</param>
        /// <returns></returns>
        /// <response code="204">The ressource is updated</response>
        /// <response code="400">one or more parameters is(are) not valid</response>
        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Put(UpdateContactCommand command)
        {
            var response = await Mediator.Send(command);
            return HttpResponseResult(response);
        }

        /// <summary>
        /// Delete a contact
        /// </summary>
        /// <remarks>
        /// Sample Request :
        ///     DELETE api/v2/contacts/
        ///     {
        ///         "Id" : 1
        ///     }
        /// </remarks>
        /// <param name="command">bind the body above into the command object</param>
        /// <returns></returns>
        /// <response code="204">The ressource is deleted</response>
        /// <response code="404">the id is not valid</response>
        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(DeleteContactCommand command)
        {
            var response = await Mediator.Send(command);
            return HttpResponseResult(response);
        }
        #endregion
        #region CRUD Contact's Skill
        /// <summary>
        ///     get all skills from a contact
        /// </summary>
        /// <remarks>
        /// Sample Request :
        ///     GET api/v2/contacts/1/skills
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Return the list of the contact's skill</response>
        /// <response code="404">the id is not valid</response>
        [HttpGet]
        [Route("{id}/skills")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        
        public async Task<IActionResult> GetSkills(int id)
        {
            var response = await Mediator.Send(new GetSkillsOfContactQuery() {Id = id});
            return HttpResponseResult(response);
        }
        /// <summary>
        ///     Add a skill to a contact
        /// </summary>
        /// <param name="id"></param>
        /// <param name="skill"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample Request
        ///     POST api/v2/contact/1/skills
        ///     {
        ///         "Name" : "C#",
        ///         "Level" : "Expert"
        ///     }
        /// </remarks>
        /// <response code="200">the skill has been bind to the contact</response>
        /// <response code="400">The skill level is not a valid level </response>

        [HttpPost]
        [Route("{id}/skills")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
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
