using Labb2ProgTemplate.RewardSystem;

namespace Labb2ProgTemplate;

public class Customer
{
    public string Name { get; private set; }

    private string? Password { get; set; }

    private List<Product> _cart;
    public List<Product> Cart => _cart;

    public RewardLevel RewardLevel { get; set; }

    public Currency Currency { get; set; }

    public Customer(string name, string? password, RewardLevel rewardLevel)
    {
        Name = name;
        Password = password;
        RewardLevel = rewardLevel;
        Currency = Currency.SEK;
        _cart = new List<Product>();
    }

    public bool CheckPassword(string password)
    {
        if (password == Password)
        {
            return true;
        }
        return false;
    }

    public void AddToCart(Product product, List<Product> productList, int amount)
    {
        if (!productList.Contains(product))
        {
            _cart.Add(product);
        }
        for (int i = 0; i < amount; i++)
        {
            product.AddAmount();
        }
    }

    public void RemoveFromCart(Product product, List<Product> productList)
    {
        Console.Clear();
        Console.WriteLine($"Hur många {product.Name.ToLower()} vill du ta bort?");
        var removalCheck = int.TryParse(Console.ReadLine(), out int amount);

        while (removalCheck)
        {
            if (removalCheck == false)
            {
                Console.WriteLine("Inkorrekt menyangivelse.");
                Thread.Sleep(1500);
                break;
            }

            if (amount <= 0)
            {
                Console.WriteLine("Du kan inte ta bort så få föremål.");
                Thread.Sleep(1500);
                break;
            }

            if (amount > product.Amount)
            {
                Console.WriteLine($"Så många {product.Name.ToLower()} har du inte i din kundkorg.");
                Thread.Sleep(1500);
                break;
            }

            if (product.Amount == 0)
            {
                _cart.Remove(product);
            }

            if (product.Amount >= 1)
            {
                for (int a = 0; a < amount; a++)
                {
                    product.RemoveAmount();

                    if (product.Amount <= 0)
                    {
                        product.Amount = 0;
                        _cart.Remove(product);
                    }
                }
            }
            Console.Clear();
            Console.WriteLine($"{amount} stycken {product.Name.ToLower()} har tagits bort från din kundkorg.");
            removalCheck = false;
        }
    }

    public static decimal CartTotal(List<Product> cart, Customer customer, Currency currency)
    {
        decimal sum = 0;
        foreach (var product in cart)
        {
            sum += (decimal)product.Price * (decimal)currency / 100 *
                (decimal)customer.RewardLevel / 100 * product.Amount;
        }
        return sum;
    }

    public string? GetPassword()
    {
        string? password = Password;
        return password;
    }

    public string GetRewardLevel()
    {
        string reward = RewardLevel.ToString();
        return reward;
    }

    public override string ToString()
    {
        var eachProduct = Cart.DistinctBy(p => p);

        string totalCart = string.Empty;

        foreach (var product in eachProduct)
        {
            string prodInfo = $"{product.Amount} st {product.Name} á {product.Price} SEK " +
                              $"/ {(decimal)product.Price * (decimal)Currency.IRR/100} IRR " +
                              $"/ {(decimal)product.Price * (decimal)Currency.EUR / 100} EUR styck\n";
            totalCart += prodInfo;
        }

        return $"Namn: {Name}\n" +
               $"Lösenord: {Password}\n" +
               $"Kundkorg:\n{totalCart}" +
               $"Rabattnivå: {RewardLevel}" +
               $"\n";
    }
}