using Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options){}
    
    public DbSet<Product> Products { get; set; }
}