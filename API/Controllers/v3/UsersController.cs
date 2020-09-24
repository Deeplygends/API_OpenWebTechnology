using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Middleware;
using Application.Features.User.Command;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace API.Controllers.v3
{
     [ApiVersion("3.0")]
    [Authorize]
    [ApiController]
    public class UsersController : BaseApiController
    {
        private IConfiguration _config;
        public UsersController(IConfiguration config)
        {
            _config = config;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("authentification")]
        public async Task<IActionResult> Authenticate([FromBody] AuthentificationCommand command)
        {
            var user = await Mediator.Send(command);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });
            var jwt = new JWTMiddleware(_config);
            user.Data.Token = jwt.GenerateSecurityToken(user.Data.IdContact.ToString());
            return Ok(user);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody] SignInCommand command)
        {
            var response = await Mediator.Send(command);
            return HttpResponseResult(response);
        }
    }
}
