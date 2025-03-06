using MediatR;
using Microsoft.AspNetCore.Mvc;
using static E.Application.CQRS.Users.Handlers.Command.Register;
using static E.Application.CQRS.Users.Handlers.Queries.GetByEmail;
namespace LibrarySystemProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    [HttpGet]
    public async Task<IActionResult> GetByEmailAsync([FromQuery] string email)
    {
        var request = new Query() { Email = email };
        return Ok(await _mediator.Send(request));
    }
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
    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        var request = new E.Application.CQRS.Users.Handlers.Command.Delete.Command() { Id = id };   
        return Ok(await _mediator.Send(request));
    }
}
