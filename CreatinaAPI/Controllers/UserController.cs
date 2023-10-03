using CreatinaAPI.Context;
using CreatinaAPI.CustomRequests;
using CreatinaAPI.Models;
using CreatinaAPI.Responses;
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

            var publicUserResponse = new PublicUserResponse
            {
                UserId = user.UserId,
                UserName = user.Name,
                UserEmail = user.Email,
            };

            return Ok(publicUserResponse);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "ocorreu um erro ao tratar a solicitação");
        }
    }

    [HttpPost]
    public ActionResult Post([FromBody] CreateUserRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Dados inválidos" + ModelState);
        }

        var userInstance = new User
        {
            Name = request.UserName,
            Email = request.UserEmail,
            Password = request.Password,
            PasswordHash = request.PasswordHash,

        };

        userInstance.SetPassword(request.Password);

        _context.Users.Add(userInstance);
        _context.SaveChanges();

        return new CreatedAtRouteResult("GetUser", new { id = userInstance.UserId }, userInstance);
    }

    [HttpPatch("{id:int}")]
    public ActionResult Patch(int id, User user)
    {
        if(id != user.UserId)
        {
            return BadRequest("foram passados dois ids diferentes");
        }

        _context.Entry(user).State = EntityState.Modified;
        _context.SaveChanges();

        return Ok(user);

    }

}
