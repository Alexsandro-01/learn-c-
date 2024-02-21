// Importar o EntityFrameworkCore para utilizar DbContext
using Microsoft.EntityFrameworkCore;

public class MyContext : DbContext
{
  // Criar um construtor que envia as configurações de inicializaçõa
  // para o construtor da classe pai DbContext
  // public MyContext(DbContextOptions<MyContext> options) : base(options)
  // {}

  //------------------//
  // Adicionamos  DbSet
  public DbSet<Category> Categories {get; set;}
  public DbSet<Product> Products {get; set;}

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    // Verificamos se o banco de dados já foi configurado
    if (!optionsBuilder.IsConfigured)
    {
      // Buscamos o valor da connection string armazenada nas váriaveis de ambiente
      // var connectionString = Environment.GetEnvironmentVariable("DOTNET_CONNECTION_STRING");

      // Executamos o método UseSqlServer e passamos a connection string
      optionsBuilder.UseSqlServer(@"
        Server=127.0.0.1;
        Database=my_context_db;
        User=SA;
        Password=ale@#3415;
        TrustServerCertificate=True;
      ");
    }
  }
}