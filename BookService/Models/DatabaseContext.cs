using BookService.Models;
using Microsoft.EntityFrameworkCore;

public class DatabaseContext : DbContext
{
  public DbSet<Author> Authors {get; set;}
  public DbSet<Book> Books {get; set;}

  public DbSet<Post> Posts {get; set;}
  public DbSet<Tag> Tags {get; set;}

  public DatabaseContext()
  {
    this.Database.EnsureCreated();
  }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    var connectionString = Environment.GetEnvironmentVariable("DOTNET_CONNECTION_STRING");

    optionsBuilder.UseSqlServer(connectionString + "Database=books_service;TrustServerCertificate=true");
  }
}