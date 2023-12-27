// See https://aka.ms/new-console-template for more information
class Program
{
  public int Num {
    get { return Num; }
    set {
      if (value > 0) {
        Num = value;
      }
    }
  }

  public static void Main() {
    Program person = new Program() {
      Num = 10
    };
    

    Console.WriteLine(person.Num);
  }
}
