using ApiSql.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiSql.Database;

public class DatabaseContext : DbContext
{
  public DbSet<Book> Books {get; set;}
  public DbSet<Author> Authors {get; set;}
  public DbSet<Publiser> Publisers {get; set;}

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    if (!optionsBuilder.IsConfigured)
    {
      var connectionString = Environment.GetEnvironmentVariable("DOTNET_CONNECTION_STRING");

      optionsBuilder.UseSqlServer(connectionString + "Database=books;TrustServerCertificate=True;");
    }
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    // Definição da relação com Author;
    modelBuilder.Entity<Book>()
      .HasOne(book => book.Author)
      .WithMany(author => author.Books)
      .HasForeignKey(book => book.AuthorId);
    
    // Definição da relação com Publisher;
    modelBuilder.Entity<Book>()
      .HasOne(book => book.Publisher)
      .WithMany(publiser => publiser.Books)
      .HasForeignKey(book => book.PubliserId);
  }
}