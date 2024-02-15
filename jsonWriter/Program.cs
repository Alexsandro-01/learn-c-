// See https://aka.ms/new-console-template for more information
using System.Text.Json;

namespace SerializeToFileAsync
{
  class Program 
  {
    public static async Task Main()
    {
      Program program = new Program();

      await program.writeFile();
      
    }

    public async Task writeFile()
    {
      Cultura cultura = new
      (
        "Banana da Terra",
        "kg",
        DateTime.Now, 
        DateTime.Now
      );

      string fileName = "db/Db.json";
      await using FileStream createStream = File.Create(fileName);
      await JsonSerializer.SerializeAsync(createStream, cultura);
    }
  }

}
