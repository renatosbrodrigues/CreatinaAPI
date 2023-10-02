using CreatinaAPI.Context;
using CreatinaAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CreatinaAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly CreatinaAPIContext _context;

    public UserController(CreatinaAPIContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<User>> Get(int page = 1, int pageSize = 6)
    {
        try
        {
            var users = _context.Users
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return Ok(users);

        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a solicitação");
        }
    }

    [HttpGet("{id:int}", Name = "GetUser")]
    public ActionResult<User> Get(int id)
    {
        try
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == id);

            if (user == null)
            {
                return NotFound("Usuário não encontrado");
            }

            return Ok(user);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "ocorreu um erro ao tratar a solicitação");
        }
    }

    [HttpPost]

    public ActionResult Post(User user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Dados inválidos" + ModelState);
        }

        var userInstance = new User
        {
            Name = user.Name,
            Email = user.Email,
            Password = user.Password,
            PasswordHash = user.PasswordHash,

        };

        userInstance.SetPassword(user.Password);
        Console.WriteLine("VERIFICACAO" + userInstance.VerifyPassword(user.PasswordHash));

        _context.Users.Add(userInstance);
        _context.SaveChanges();

        return new CreatedAtRouteResult("GetUser", new { id = user.UserId }, user);
    }
}
