using B.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace D.DAL.SqlServer.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books { get; set; }
}
