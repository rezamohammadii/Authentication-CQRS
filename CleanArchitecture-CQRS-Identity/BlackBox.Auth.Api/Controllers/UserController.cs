using BlackBox.Auth.Application.Commands.User.Create;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlackBox.Auth.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Create")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<IActionResult> CreateUser(CreateUserCommand userCommand)
            => Ok(await _mediator.Send(userCommand));

        [HttpGet("GetAll")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<IActionResult> GetAllUserAsync()
        {
            return Ok(await _mediator.Send(new Application.Queries.User.GetUserQuery()));
        }
    }
}
