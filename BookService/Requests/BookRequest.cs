using System.ComponentModel.DataAnnotations;
using BookService.Models;

namespace BookService.Requests;

public class BookRequest
{
  [Required]
  public string Title {get; set;}
  public int Year {get; set;}
  public decimal Price {get; set;}
  public string Genre {get; set;}

  // Foreign key
  public int AuthorId {get; set;}
  // Navigation Property
  public Author? Author {get; set;}
}