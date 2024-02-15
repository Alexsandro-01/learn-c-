// See https://aka.ms/new-console-template for more information
 List<Car> cars = new List<Car>
{
    new Car { Brand = "Ferrari", Model = "LaFerrari", Price = 7100000 },
    new Car { Brand = "McLaren", Model = "Elva", Price = 8600000 },
    new Car { Brand = "Bentley", Model = "Mulliner Batur", Price = 10200000 },
    new Car { Brand = "Aston Martin", Model = "Valkyrie", Price = 16300000 },
    new Car { Brand = "BMW", Model = "IX", Price = 670000 },
    new Car { Brand = "Bugatti", Model = "Divo", Price = 26000000 },
    new Car { Brand = "Ferrari", Model = "F8 Spider", Price = 5200000 },
    new Car { Brand = "McLaren", Model = "Speedtail", Price = 40000000 },
    new Car { Brand = "Koenigsegg", Model = "Agera", Price = 7500000 },
    new Car { Brand = "Devel", Model = "Sixteen", Price = 79000020 }
};

// #### Clausula SELECT
// var carsName = cars.Select(car => car.Brand + " " + car.Model);

// foreach(var carName in carsName)
// {
//   Console.WriteLine(carName);
// }


// #### Clausula WHERE
// IEnumerable<Car> cars1 = cars.Where(car => car.Brand == "Ferrari");

// foreach (var car in cars1)
// {
//   Console.WriteLine($"{car.Brand} {car.Model} - Preço: R$ {car.Price}");
// }


// #### Clausula de ORDENAÇÃO  ORDERBY
//  IEnumerable<Car> result = cars.OrderBy(car => car.Price);

//  foreach (var car in result)
//  {
//     Console.WriteLine($"{car.Brand} {car.Model} - Preço: R$ {car.Price}");
//  }


// #### Clausula GROUPBY
IEnumerable<IGrouping<String, Car>> resultGroupBy = cars.GroupBy(car => car.Brand);

foreach (var item in resultGroupBy)
{
  Console.WriteLine(item.Key);
  foreach (var car in item)
  {
    Console.WriteLine($"\t {car.Model} - Preço: {car.Price}");
  }
}