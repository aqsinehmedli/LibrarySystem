using MediatR;
using Microsoft.AspNetCore.Mvc;
using static E.Application.CQRS.Users.Handlers.Command.Register;
namespace LibrarySystemProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Register([FromQuery] E.Application.CQRS.Users.Handlers.Command.Register.Command request)
    {
        return Ok(await _mediator.Send(request));
    }
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] E.Application.CQRS.Users.Handlers.Command.Update.Command request)
    {
        return Ok(await _mediator.Send(request));
    }
}
