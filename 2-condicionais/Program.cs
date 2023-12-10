// See https://aka.ms/new-console-template for more information
class Program {
  public static void Main() {
    Program.ForEachExample();
  }

  public static void ForEachExample() {
    string[] names = new string[] {"Hulk", "Thor", "Loki"};

    foreach( var name in names) {
      Console.WriteLine(name);
    }
  }
}
