using System.ComponentModel.DataAnnotations;
using BookService.Requests;

namespace BookService.Models;

public class Book
{
  public Book(BookRequest bookRequest)
  {
    Author = bookRequest.Author;
    Title = bookRequest.Title;
    Genre = bookRequest.Genre;
    AuthorId = bookRequest.AuthorId;
    Year = bookRequest.Year;
    Price = bookRequest.Price;
  }

  public Book() {}

  public int Id { get; set; }
  [Required]
  public string Title { get; set; }
  public int Year { get; set; }
  public decimal Price { get; set; }
  public string Genre { get; set; }

  // Foreign key
  public int AuthorId { get; set; }
  // Navigation Property
  public Author Author { get; set; }
}