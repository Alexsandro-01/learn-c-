using System.ComponentModel.DataAnnotations;

namespace BookService.Models;

public class Author
{
  public Author(AuthorRequest author)
  {
    Name = author.Name;
  }

  public Author() {}

  public int Id {get; set;}
  [Required]
  public string Name {get; set;}
  public ICollection<Book> Books {get;}
}