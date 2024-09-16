// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

public class Program
{
  static void Main(string[] args)
  {
    using (var db = new MyContext())
    {
      db.Database.EnsureCreated();

      // // create and save a new Category
      // Console.WriteLine("Enter a name for a new Category:");
      // var name = Console.ReadLine();

      // var category = new Category { Name = name };
      // db.Categories.Add(category);
      // db.SaveChanges();

      // // Display all Categories from the database
      // var query = from b in db.Categories
      //             orderby b.Name
      //             select b;

      // Console.WriteLine("All Categories in the database:");
      // foreach (var item in query)
      // {
      //   Console.WriteLine(item.Name);
      // }

      // Console.WriteLine("Press any key to exit...");
      // Console.ReadKey();

      // Post post = new() { Text = "Example of a post's text." };
      // Tag tag = new() { Name = "Politic" };
      // Tag tag1 = new() { Name = "Sports" };
      // post.Tags.Add(tag);
      // tag.Posts.Add(post);
      // db.Add(post);
      var post = db.Posts.Where(p => p.Id == 2);
      if (post.IsNullOrEmpty())
        Console.Write("Not found!");

      var tag = db.Tags.Where(t => t.Id == 3);
      var tagList = tag.ToList();
      
      var postList = post.ToList();
      postList[0].Tags.Add(tagList[0]);

      db.Update(postList[0]);
      db.SaveChanges();
      var posts = db.Posts.Include(p => p.Tags).ToList();

      Console.Write(posts.ToString());
    }
  }
}
