using ApiSql.Models;
using ApiSql.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ApiSql.Controllers;

[ApiController]
[Route("[Controller]")]
public class BookController : ControllerBase
{
  private readonly BookRepository _repository;
  public BookController(BookRepository repository)
  {
    _repository = repository;
  }

  [HttpPost]
  public IActionResult AddBook()
  {
    var book = new Book
    {
      Title = "The Divine Comedy",
      Description = "A journey through the infinite torment of hell",
      Year = 2013,
      Pages = 831,
      Genre = "Drama",
      Author = new Author
      {
        Name = "Dante Alighieri",
        Email = "mail@mail.com"
      },
      Publisher = new Publiser
      {
        Name = "Paradise Publisher"
      }
    };

    _repository.Add(book);

    return Ok(new { message = "Book added" });
  }

  [HttpGet]
  public IActionResult GetBooks()
  {
    var result = _repository.GetBookList();

    return Ok(result);
  }

  [HttpGet("{id}")]
  public IActionResult GetById(int id)
  {
    var book = _repository.GetById(id);

    if (book is null)
    {
      return NotFound(new {
        message = "Book not found"
      });
    }

    return Ok(book);
  }
}