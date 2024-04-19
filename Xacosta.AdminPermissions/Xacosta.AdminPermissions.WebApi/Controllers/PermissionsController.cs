using MediatR;
using Microsoft.AspNetCore.Mvc;
using Xacosta.AdminPermissions.Application.Feature;

namespace Xacosta.AdminPermissions.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController(ISender _sender) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(RequestPermissionsCommand command)
        {
            var prestamo = await _sender.Send(command);
            return Ok(prestamo);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(ModifyPermissionsCommand command)
        {
            var prestamo = await _sender.Send(command);
            return Ok(prestamo);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var prestamo = await _sender.Send(new GetPermissionsQuery());
            return Ok(prestamo);
        }
    }
}
