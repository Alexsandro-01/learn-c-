using ApiSql.Database;
using ApiSql.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiSql.Repositories;

public class BookRepository
{
  private readonly DatabaseContext _databaseContext;

  public BookRepository(DatabaseContext databaseContext)
  {
    _databaseContext = databaseContext;
    _databaseContext.Database.EnsureCreated();
  }

  public Book Add(Book book)
  {
    // Sempre utilizamos o contexto de banco de dados que
    // é um membro da classe BookRepository;
    _databaseContext.Add(book);
    _databaseContext.SaveChanges();
    return book;
  }

  // Para realizar a consulta, adicionamos um método
  // que realiza essa consulta em BookRepository;
  public List<Book> GetBookList()
  {
    var query = _databaseContext
                .Books
                .ToList();

    return query;
  }

  // Agora, vamos dizer que queremos recuperar o título do
  // livro, o nome do autor e o nome da editora com o id 3:
  public Book GetById(int id)
  {
    return _databaseContext.Books
           .Where(e => e.BookId == id)
           .Include(e => e.Author)
           .Include(e => e.Publisher)
           .First();
  }

  public virtual void Update(Book book)
  {
    _databaseContext.Update(book);
    _databaseContext.SaveChanges();
  }

  public virtual void Delete(int id)
  {
    // realizamos uma busca em nossa base para encontrarmos
    // o livro e suas associações, pelo id do livro, que 
    // gostaríamos de excluir
    var book = _databaseContext.Books
      .Include(e => e.Author)
      .Include(e => e.Publisher)
      .Single(p => p.BookId == id);
    
    _databaseContext.Remove(book); // removemos o livro;
    _databaseContext.Remove(book.Author); // removemoso autor relacionado;
    _databaseContext.Remove(book.Publisher); // remove a editora relacionada;
    _databaseContext.SaveChanges(); // salvamos nossa exclusão;
  }
}