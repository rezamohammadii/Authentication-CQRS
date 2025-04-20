using BlackBox.Auth.Application.Commands.User.Create;
using BlackBox.Auth.Application.Commands.User.Delete;
using BlackBox.Auth.Application.Commands.User.Update;
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

        [HttpGet("GetUserDetailsByUserName/{userName}")]
        [ProducesDefaultResponseType(typeof(UserDetailsResponseDTO))]
        public async Task<IActionResult> GetUserDetailsByUserName(string userName)
        {
            var result = await _mediator.Send(new GetUserDetailsByUserNameQuery() { UserName = userName });
            return Ok(result);
        }

        [HttpPost("AssignRoles")]
        [ProducesDefaultResponseType(typeof(int))]

        public async Task<ActionResult> AssignRoles(AssignUsersRoleCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("EditUserRoles")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<IActionResult> EditUserRoles(UpdateUserRolesCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("GetAllUserDetails")]
        [ProducesDefaultResponseType(typeof(UserDetailsResponseDTO))]
        public async Task<IActionResult> GetAllUserDetails()
        {
            var result = await _mediator.Send(new GetAllUsersDetailsQuery());
            return Ok(result);
        }

        [HttpPut("EditUserProfile/{id}")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<IActionResult> EditUserProfile(string id, [FromBody] EditUserProfileCommand command)
        {
            if (id == command.Id)
            {
                var result = await _mediator.Send(command);
                return Ok(result);

            }
            else
            {
                return BadRequest();
            }
        }

    }
}
