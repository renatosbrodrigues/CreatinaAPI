using CreatinaAPI.Context;
using CreatinaAPI.CustomRequests;
using Microsoft.AspNetCore.Mvc;
using CreatinaAPI.Tokens;

namespace CreatinaAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly CreatinaAPIContext _context;

    public AuthController(CreatinaAPIContext context)
    {
        _context = context;
    }

    [HttpPost("login")]
    public ActionResult Login([FromBody] LoginRequest request)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == request.Email);
        var auth = user.VerifyPassword(request.Password);

        if(!auth)
        {
            return StatusCode(StatusCodes.Status401Unauthorized, "Email ou senha incorretos");
        }
        else
        {
            var token = TokenGenerator.GenerateToken(request.Email);
            return Ok(new {Token = token});
        }

    }

}