using ManagementSystem1.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<Employee> Employees { get; set; }
}