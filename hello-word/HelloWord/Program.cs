// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

// Calcula quantos metros quadrados possui o cômodo multiplicando width por length.
Console.WriteLine("Digite a largura do cômodo:");
int width = int.Parse(Console.ReadLine());

Console.WriteLine("Agora, digite a altura do cômodo:");
int length = int.Parse(Console.ReadLine());

// Calcula o quociente X dividindo a potência da lâmpada que será utilizada por 18, quantidade necessária para iluminar 1 metro quadrado.
int wTotalNeeded = (width * length) * 18;

// Calcula a quantidade de lâmpadas necessárias dividindo o total de metros quadrados do cômodo pelo quociente X.

int numberOfLamps = wTotalNeeded / 18;

Console.WriteLine("A quantidade de lâmpadas necessárias é " + numberOfLamps);