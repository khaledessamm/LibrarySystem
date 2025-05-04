using Microsoft.EntityFrameworkCore;
using LibrarySystem.Models;
using System.Reflection;

namespace LibrarySystem.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public DbSet<Book> Books { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); 

        var cascadefks = modelBuilder.Model
            .GetEntityTypes()
            .SelectMany(t => t.GetForeignKeys())
            .Where(fk => fk.DeleteBehavior == DeleteBehavior.Cascade && !fk.IsOwnership);

        foreach (var fk in cascadefks)
            fk.DeleteBehavior = DeleteBehavior.SetNull; //setnull here to prevent deleting the books  while  deleting the category

        base.OnModelCreating(modelBuilder);

    }
}

