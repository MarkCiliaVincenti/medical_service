using Microsoft.AspNetCore.Mvc;
using Domain;

namespace Api;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly UserService _service;

    public UserController(ILogger<UserController> logger, UserService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet("checkUser")]
    async public Task<ActionResult> UserExists(string login)
    {
        var result = await _service.UserExists(login);
        
        if (result.Success)
        {
            return Ok();
        }

        return Problem(statusCode:(int)StatusCodes.NotFound, detail: result.Error);
    }

    [HttpGet("getUser")]
    async public Task<ActionResult<UserView>> GetUserByLogin(string login)
    {
        var result = await _service.GetUserByLogin(login);

        if (result.Success)
        {   
            return Ok(new UserView(result.Value));
        }

        return Problem(statusCode:(int)StatusCodes.NotFound, detail: result.Error);
    }

    [HttpPost("createUser")]
    async public Task<ActionResult<UserView>> CreateUser(UserForm form)
    {
        var result = await _service.CreateUser(form);

        if (result.Success)
        {
            return Ok(new UserView(result.Value));
        }

        return Problem(statusCode:(int)StatusCodes.NotFound, detail: result.Error);
    }
}