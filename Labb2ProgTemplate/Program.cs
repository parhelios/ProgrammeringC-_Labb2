using Labb2ProgTemplate;
using Labb2ProgTemplate.RewardSystem;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

var shop = new Shop();

shop.PrintTitle(1);
shop.PrintTitle(2);
shop.PrintTitle(3);
shop.PrintTitle(4);
shop.PrintTitle(5);
shop.PrintTitle(6);
shop.PrintTitle(7);
shop.MainMenu();

////GRATIS testkod för att spara dig tid

//var gustavWasa = new Customer("Gustav Wasa", "123", RewardLevel.Standard);

//var sko = new Product("Sko", 432);
//var älg = new Product("Älg", 666);
//var ryssland = new Product("Ryssland", 1);

//var lista = new List<Product>();
//gustavWasa.AddToCart(sko, lista, 4);
//gustavWasa.AddToCart(älg, lista, 2);
//gustavWasa.AddToCart(ryssland, lista, 1);

//Console.WriteLine(gustavWasa.ToString());