// See https://aka.ms/new-console-template for more information
class Imc
{
  private int height = 0;
  private double weight = 0;

  public static void Main()
  {
    Imc imc = new();

    imc.GetHeight();
    imc.GetWeight();

    double heightInMeters = (double)imc.height / 100;

    double userImcResult = imc.weight / (heightInMeters * heightInMeters);

    Console.WriteLine($"Your IMC is {userImcResult:N2}");
  }

  public void GetHeight()
  {
    bool canConvet = false;
    

    while (!canConvet)
    {
      Console.WriteLine("Type your height in centimeters:");

      string? userHeight = Console.ReadLine();

      canConvet = Int32.TryParse(userHeight, out height);

      if (!canConvet) {
        Console.WriteLine("Invalid value.");

      }
    }
  }

  public void GetWeight() {
    bool canConvet = false;

    while (!canConvet) {
      Console.WriteLine("Type your weight in kg:");

      string? userWeight = Console.ReadLine();

      if (userWeight == null) {
        Console.WriteLine("Type a value!");
        
      } else {
        canConvet = double.TryParse(userWeight.Replace(',', '.'), out weight);

        if (!canConvet) {
          Console.WriteLine("Invalid value.");
        }
      }

    }
  }
}
