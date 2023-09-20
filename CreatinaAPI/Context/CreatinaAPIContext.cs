using CreatinaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CreatinaAPI.Context;

public class CreatinaAPIContext : DbContext
{
    public CreatinaAPIContext(DbContextOptions<CreatinaAPIContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Creatine> Creatines { get; set; }
}
