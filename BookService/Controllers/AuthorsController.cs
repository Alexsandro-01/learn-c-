using BookService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BookService.Controllers;

[ApiController]
[Route("[Controller]")]
public class AuthorsController : ControllerBase
{
  private readonly DatabaseContext _context;
  public AuthorsController(DatabaseContext dbContext)
  {
    _context = dbContext;
  }

  [HttpPost]
  public IActionResult AddAuthor(AuthorRequest authorRequest)
  {
    Author author = new(authorRequest);

    _context.Add(author);
    _context.SaveChanges();

    return Created("Authors", author);
  }

  [HttpGet("{id}")]
  public IActionResult GetById(int id)
  {
    var author = _context.Authors.Where(a => a.Id == id).Include(b => b.Books);

    if (author.IsNullOrEmpty()) 
      return NotFound(new { message = "Author not found"});

    return Ok(author);
  }
}