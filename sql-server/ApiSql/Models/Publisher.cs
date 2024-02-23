using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiSql.Models;

public class Publiser
{
  [Key]
  public int PubliserId {get; set;}
  public string Name {get; set;}
  [InverseProperty("Publisher")]
  public ICollection<Book> Books {get; set;}
}