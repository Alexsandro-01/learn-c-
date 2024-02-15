
namespace SerializeToFileAsync
{
  class Cultura
  {
    public string Name {get; set;}
    public string Unidade {get; set;}
    public DateTime CreatedAt {get; set;}
    public DateTime UpdatedAt {get; set;}

    public Cultura(string name, string unidade, DateTime createdAt, DateTime updatedAt)
    {
      Name = name;
      Unidade = unidade;
      CreatedAt = createdAt;
      UpdatedAt = updatedAt;
    }
  }
}