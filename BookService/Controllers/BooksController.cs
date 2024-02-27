using BookService.Models;
using BookService.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BookService.Controllers;

[ApiController]
[Route("[Controller]")]
public class BooksController : ControllerBase
{
  protected readonly DatabaseContext _context;

  public BooksController(DatabaseContext databaseContext)
  {
    _context = databaseContext;
  }

  [HttpPost]
  public IActionResult AddBook(BookRequest bookRequest)
  {
    var author = _context.Authors.Where(a => a.Id == bookRequest.AuthorId);

    if (author.IsNullOrEmpty())
    {
      return NotFound(new {
        message = "Author not found, book not inserted."
      });
    }

    Book book = new(bookRequest);

    _context.Add(book);
    _context.SaveChanges();

    return Created("Books", book);
  }

  [HttpGet("{id}")]
  public IActionResult GetById(int id)
  {
    var book = _context
               .Books
               .Where(b => b.Id == id)
               .Include(b => b.Author);

    if (book.IsNullOrEmpty())
      return NotFound(new { message = "Book not found"});

    return Ok(book);
  }
}