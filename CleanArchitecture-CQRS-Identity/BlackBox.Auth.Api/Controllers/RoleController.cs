using BlackBox.Auth.Application.Commands.Role.Create;
using BlackBox.Auth.Application.Commands.Role.Delete;
using BlackBox.Auth.Application.Commands.Role.Update;
using BlackBox.Auth.Application.DTOs;
using BlackBox.Auth.Application.Queries.Role;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlackBox.Auth.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        public readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Create")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<IActionResult> CreateRoleAsync(RoleCreateCommand command)
            => Ok( await _mediator.Send(command));

        [HttpGet("GetAll")]
        [ProducesDefaultResponseType(typeof(List<RoleResponseDTO>))]
        public async Task<IActionResult> GetRoleAsync()
            => Ok(await _mediator.Send(new GetRoleQuery()));

        [HttpGet("{id}")]
        [ProducesDefaultResponseType(typeof(RoleResponseDTO))]
        public async Task<IActionResult> GetRoleByIdAsync(string id)
            => Ok(await _mediator.Send(new GetRoleByIdQuery() { RoleId = id }));
        

        [HttpDelete("Delete/{id}")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<IActionResult> DeleteRoleAsync(string id)
            => Ok(await _mediator.Send(new DeleteRoleCommand() { RoleId = id}));

        [HttpPut("Edit/{id}")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<IActionResult> EditRoleAsync(string id, [FromBody] UpdateRoleCommand command)
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
