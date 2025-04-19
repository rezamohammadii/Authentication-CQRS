using BlackBox.Auth.Application.Commands.User.Create;
using BlackBox.Auth.Application.Commands.User.Delete;
using BlackBox.Auth.Application.DTOs;
using BlackBox.Auth.Application.Queries.User;
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
            => Ok(await _mediator.Send(new Application.Queries.User.GetUserQuery()));


        [HttpDelete("Delete/{userId}")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var result = await _mediator.Send(new DeleteUserCommand() { Id = userId });
            return Ok(result);
        }

        [HttpGet("GetUserDetails/{userId}")]
        [ProducesDefaultResponseType(typeof(UserDetailsResponseDTO))]
        public async Task<IActionResult> GetUserDetails(string userId)
        {
            var result = await _mediator.Send(new GetUserDetailsQuery() { UserId = userId });
            return Ok(result);
        }


    }
}
