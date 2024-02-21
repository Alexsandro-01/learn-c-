// See https://aka.ms/new-console-template for more information
public class Program
{
  static void Main(string[] args)
  {
    using (var db = new MyContext())
    {
      db.Database.EnsureCreated();

      // create and save a new Category
      Console.WriteLine("Enter a name for a new Category:");
      var name = Console.ReadLine();

      var category = new Category { Name = name };
      db.Categories.Add(category);
      db.SaveChanges();

      // Display all Categories from the database
      var query = from b in db.Categories
                  orderby b.Name
                  select b;

      Console.WriteLine("All Categories in the database:");
      foreach (var item in query)
      {
        Console.WriteLine(item.Name);
      }

      Console.WriteLine("Press any key to exit...");
      Console.ReadKey();
    }
  }
}
