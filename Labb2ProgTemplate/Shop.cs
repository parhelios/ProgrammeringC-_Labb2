using System.Diagnostics;
using System.Text.RegularExpressions;
using Labb2ProgTemplate.RewardSystem;

namespace Labb2ProgTemplate;

public class Shop
{
    private List<Customer?> CustomerList { get; set; } = new();

    private Customer? CurrentCustomer { get; set; }

    private List<Product> Products { get; set; } = new();

    public Shop()
    {
        
        Products.Add(new Product("Banan", 25));
        Products.Add(new Product("Melon", 50));
        Products.Add(new Product("Kiwi", 75));
        Products.Add(new Product("Citron", 100));

        var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "parheliosLabb2.txt");

        if (!File.Exists(directory))
        {
            CustomerList.Add(new BronzeCustomer("Knatte", "123"));
            CustomerList.Add(new SilverCustomer("Fnatte", "321"));
            CustomerList.Add(new GoldCustomer("Tjatte", "213"));
            CustomerList.Add(new Customer("Kenneth", "666", RewardLevel.Standard));

            WriteToNewFile();
        }

        if (File.Exists(directory))
        {
            ReadFromFile();
        }
    }

    public void MainMenu()
    {
        void Case3()
        {
            Console.Clear();
            Console.WriteLine("Skäms på sig! Här får man inte vara!");
            Process.Start(new ProcessStartInfo { FileName = "https://www.youtube.com/watch?v=dQw4w9WgXcQ", UseShellExecute = true });
            Environment.Exit(0);
            Console.ReadKey(true);
        }

        void Case4()
        {
            WriteToNewFile();
            Console.WriteLine("Programmet avslutas.");
            Environment.Exit(0);
            Console.ReadKey(true);
        }

        var mainMenuNav = new Dictionary<Enum, Action>()
        {
            { ConsoleKey.D1, Login },
            { ConsoleKey.D2, Register },
            { ConsoleKey.D3, Case3},
            { ConsoleKey.D4, Case4}
        };

        while (true)
        {
            Console.Clear();
            Console.WriteLine();
            PrintTitle(8);

            Console.WriteLine("Potion Seller, I am going into battle. \nAnd I want your strongest potions!");
            Console.WriteLine();
            Console.WriteLine("Navigera med siffrorna:");
            Console.WriteLine("\t1) Logga in.\ud83d\udcbb ");
            Console.WriteLine("\t2) Registrera ny kund.\ud83d\udcbe ");
            Console.WriteLine("\t3) Suspekta handlingar och illegal information.\u2620\ufe0f");
            Console.WriteLine("\t4) Avsluta program.\ud83d\uded1");
            var menuNav = Console.ReadKey(true).Key;

            if (mainMenuNav.ContainsKey(menuNav))
            {
                mainMenuNav[menuNav]();
                break;
            }

            if (!mainMenuNav.ContainsKey(menuNav))
            {
                Console.WriteLine("Inkorrekt menyval.");
                Console.ReadKey(true);
            }
        }
    }

    private void Login()
    {
        Console.Clear();

        Console.WriteLine("Logga in");
        Console.WriteLine();
        Console.WriteLine("Skriv \"<>\" för att komma tillbaka till huvudmenyn.");
        Console.WriteLine();
        Console.WriteLine("Ange användarnamn:");
        string? customerName = Console.ReadLine();
        Console.WriteLine();

        if (customerName == "<>")
        {
            MainMenu();
        }

        while (true)
        {
            foreach (var customer in CustomerList)
            {
                if (!CustomerList.Any(c => c.Name.ToLower() == customerName.ToLower()))
                {
                    Console.Clear();
                    Console.WriteLine("Användaren finns inte registrerad.");
                    Console.WriteLine("Vill du registrera en ny användare?");
                    Console.Write("Skriv \"J\" eller \"N\": ");
                    var regNewUser = Console.ReadKey(true).Key;

                    var regUser = new Dictionary<Enum, Action>()
                    {
                        {ConsoleKey.J, Register},
                        {ConsoleKey.N, MainMenu}
                    };

                    if (regUser.ContainsKey(regNewUser))
                    {
                        regUser[regNewUser]();
                    }
                }

                if (customer?.Name.ToLower() == customerName?.ToLower())
                {
                    Console.WriteLine("Ange lösenord:");
                    var customerPass = Console.ReadLine();

                    if (customerPass == "<>")
                    {
                        MainMenu();
                    }

                    if (customer != null && customer.CheckPassword(customerPass))
                    {
                        CurrentCustomer = customer;
                        PreShop();
                        break;
                    }
                    if (customer != null && customer.CheckPassword(customerPass) == false)
                    {
                        Console.WriteLine("Inkorrekt lösenord. Försök igen!");
                        Console.WriteLine("Skriv \"<>\" för att komma tillbaka till huvudmenyn.");
                        Thread.Sleep(1000);
                        Console.Clear();
                        break;
                    }
                }
            }
        }
    }

    private void Register()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Registrera ny användare.");
            Console.WriteLine();
            Console.WriteLine("Skriv \"<>\" för att komma tillbaka till huvudmenyn.");
            Console.WriteLine();
            Console.WriteLine("Ange användarnamn:");
            var customerName = Console.ReadLine();
            Console.WriteLine();

            if (customerName == "<>")
            {
                MainMenu();
                break;
            }

            if (CustomerList.Any(t => customerName?.ToLower() == t?.Name.ToLower()))
            {
                Console.WriteLine("Användaren finns redan registrerad.");
                Console.WriteLine("Återgår till huvudmenyn.");
                Console.ReadKey(true);

                MainMenu();
                break;
            }

            if (Regex.IsMatch(customerName, @"^\d+$"))
            {
                Console.WriteLine("Användarnamnet får inte innehålla några siffror.");
                Console.WriteLine("Återgår till huvudmenyn.");
                Thread.Sleep(1500);

                MainMenu();
                break;
            }

            if (customerName.Length <= 3)
            {
                Console.WriteLine("Användarnamnet är för kort.");
                Console.WriteLine("Återgår till huvudmenyn.");
                Thread.Sleep(1500);

                MainMenu();
                break;
            }

            Console.WriteLine("Ange lösenord:");
            var customerPass = Console.ReadLine();

            if (customerName != null)
            {
                if (customerPass != String.Empty)
                {
                    if (customerPass.Length <= 2)
                    {
                        Console.WriteLine("Lösenordet är för kort.");
                        Console.WriteLine("Återgår till huvudmenyn.");
                        Thread.Sleep(1500);

                        MainMenu();
                        break;
                    }

                    Console.WriteLine();
                    CustomerList.Add(new Customer(customerName, customerPass, RewardLevel.Standard));
                    Console.WriteLine("Användare registrerad. Tryck Enter för att återvända till huvudmenyn.");
                    Console.ReadKey(true);

                    MainMenu();
                    break;
                }

                Console.WriteLine("Användarinformation ofullständig. Försök igen.");
                Console.ReadKey(true);
            }
        }
    }

    private void PreShop()
    {
        void Case4()
        {
            Console.Clear();
            Console.WriteLine("Varukorgen töms och du loggas ut.");
            CurrentCustomer?.Cart.Clear();

            foreach (var product in Products)
            {
                product.Amount = 0;
            }

            Thread.Sleep(2500);
            MainMenu();
        }

        void Case5()
        {
            while (true)
            {
                Console.Clear();
                var selectCurrency = new Dictionary<ConsoleKey, Currency>
                {
                    { ConsoleKey.D1, Currency.SEK },
                    { ConsoleKey.D2, Currency.EUR },
                    { ConsoleKey.D3, Currency.IRR }
                };

                Console.WriteLine("Vilken valuta vill du se i butiken?");
                Console.WriteLine("\t1) SEK");
                Console.WriteLine("\t2) EURO");
                Console.WriteLine("\t3) Iranska Real (IRR)");
                var menuNav = Console.ReadKey(true).Key;

                if (selectCurrency.TryGetValue(menuNav, out var value))
                {
                    CurrentCustomer.Currency = value;
                    PreShop();
                    break;
                }

                if (!selectCurrency.ContainsKey(menuNav))
                {
                    Console.WriteLine("Inkorrekt menyval.");
                    Console.ReadKey(true);
                }
            }
        }

        var preShopNav = new Dictionary<Enum, Action>
        {
            { ConsoleKey.D1, ShopMenu },
            { ConsoleKey.D2, ViewCart },
            { ConsoleKey.D3, Checkout },
            { ConsoleKey.D4, Case4 },
            { ConsoleKey.D0, Case5 },
        };

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Huvudmeny");
            Console.WriteLine();
            Console.WriteLine($"Rabattnivå: {CurrentCustomer.GetRewardLevel()}");
            Console.WriteLine($"Valuta: {CurrentCustomer.Currency}");
            Console.WriteLine();
            Console.WriteLine("Navigera med siffrorna:");
            Console.WriteLine("\t1) Handla.");
            Console.WriteLine("\t2) Visa kundvagn.");
            Console.WriteLine("\t3) Gå till kassan.");
            Console.WriteLine("\t4) Logga ut.");
            Console.WriteLine();
            Console.WriteLine("\t0) Ändra valuta.");
            var menuNav = Console.ReadKey(true).Key;

            if (preShopNav.ContainsKey(menuNav))
            {
                preShopNav[menuNav]();
                break;
            }

            if (!preShopNav.ContainsKey(menuNav))
            {
                Console.WriteLine("Inkorrekt menyval.");
                Console.ReadKey(true);
            }
        }
    }

    private void ShopMenu()
    {
        Console.Clear();
        var shopMenuNav = new Dictionary<Enum, Action?>
        {
            { ConsoleKey.D1, () => MenuItem(1) },
            { ConsoleKey.D2, () => MenuItem(2) },
            { ConsoleKey.D3, () => MenuItem(3) },
            { ConsoleKey.D4, () => MenuItem(4) },
            { ConsoleKey.D0, PreShop}
        };

        while (true)
        {
            Console.Clear();

            var counter = 1;
            decimal unitCost = 0;
            Console.WriteLine("Produkter som finns i butiken: ");
            foreach (var product in Products)
            {
                unitCost = (decimal)product.Price * (decimal)CurrentCustomer.Currency / 100 *
                    (decimal)CurrentCustomer.RewardLevel / 100;
                unitCost = Math.Round(unitCost, 2);

                Console.WriteLine($"{counter}) {product.Name}: {unitCost} {CurrentCustomer.Currency} ");
                counter++;
            }

            Console.WriteLine();
            Console.WriteLine("0) Återgå till huvudmeny.");
            Console.WriteLine();
            Console.WriteLine("Välj alternativ genom att ange dess korrelerande nummer!");
            var menuNav = Console.ReadKey(true).Key;


            if (shopMenuNav.ContainsKey(menuNav))
            {
                shopMenuNav[menuNav]();
                continue;
            }

            if (!shopMenuNav.ContainsKey(menuNav))
            {
                Console.WriteLine("Inkorrekt menyval.");
                Thread.Sleep(1500);
            }
        }
    }

    private void ViewCart()
    {
        if (Products[0].Amount == 0 && Products[1].Amount == 0 && Products[2].Amount == 0 && Products[3].Amount == 0)
        {
            Console.Clear();
            Console.WriteLine("Du har inga produkter i din varukorg.");
            Console.WriteLine("Tryck Enter för att återgå till huvudmenyn.");
            Console.ReadKey(true);
            PreShop();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Din varukorg innehåller: ");
            decimal unitCost = 0;
            int prodCounter = 1;

            CurrentCustomer?.Cart.ForEach(products =>
            {
                unitCost = (decimal)products.Price * (decimal)CurrentCustomer.Currency / 100 *
                    (decimal)CurrentCustomer.RewardLevel / 100;
                unitCost = Math.Round(unitCost, 2);

                Console.WriteLine($"{prodCounter}) {products.Amount}st {products.Name}, {unitCost} {CurrentCustomer.Currency}/st = {unitCost * products.Amount} {CurrentCustomer.Currency}");

                prodCounter++;
            });
            Console.WriteLine();
            Console.WriteLine($"Totalkostnad för varukorgen: {Customer.CartTotal(CurrentCustomer?.Cart, CurrentCustomer, CurrentCustomer.Currency)} {CurrentCustomer.Currency}");
            Console.WriteLine();
            Console.WriteLine("Om du vill ta bort något ur varukorgen, skriv dess varukorgsnummer nedan.");
            Console.WriteLine("Annars skriv \"<>\" för att återgå till huvudmenyn.");

            string? inputCheck = Console.ReadLine();
            bool removeCheck = int.TryParse(inputCheck, out var removeInput);

            if (inputCheck == "<>")
            {
                PreShop();
            }

            while (removeCheck)
            {
                if (removeInput > CurrentCustomer?.Cart.Count)
                {
                    Console.Clear();
                    Console.WriteLine("Inkorrekt menyval.");
                    Thread.Sleep(1500);
                    ViewCart();
                    break;
                }

                if (removeInput <= CurrentCustomer?.Cart.Count)
                {
                    CurrentCustomer?.RemoveFromCart(CurrentCustomer.Cart[removeInput - 1], CurrentCustomer.Cart);
                    Console.ReadKey(true);
                    break;
                }
            }
            PreShop();
        }
    }

    private void Checkout()
    {
        if (CurrentCustomer?.Cart.Count == 0)
        {
            Console.Clear();
            Console.WriteLine("Du har inga produkter i din varukorg.");
            Console.WriteLine("Tryck Enter för att återgå till huvudmenyn.");
            Console.ReadKey(true);
            PreShop();
        }

        Console.Clear();
        Console.WriteLine("Din varukorg innehåller: ");

        decimal unitCost = 0;
        int prodCounter = 1;

        CurrentCustomer?.Cart.ForEach(products =>
        {
            unitCost = (decimal)products.Price * (decimal)CurrentCustomer.Currency / 100 *
                (decimal)CurrentCustomer.RewardLevel / 100;
            unitCost = Math.Round(unitCost, 2);

            Console.WriteLine($"{prodCounter}) {products.Amount}st {products.Name}, {unitCost} {CurrentCustomer.Currency}/st = {unitCost * products.Amount} {CurrentCustomer.Currency}");

            prodCounter++;
        });

        Console.WriteLine();
        Console.WriteLine($"Totalkostnad för varukorgen: {Customer.CartTotal(CurrentCustomer?.Cart, CurrentCustomer, CurrentCustomer.Currency)} {CurrentCustomer.Currency}");
        Console.WriteLine("Vi accepterar enbart Dogecoin eller mufflonfår som betalmedel.");
        Console.WriteLine("Fråga på WWW för exakt växelkurs.");
        Console.WriteLine();

        while (true)
        {
            Console.WriteLine($"Vill du checka ut, skriv \"J\". För att återgå till huvudmenyn, tryck \"N\" ");
            var menuNav = Console.ReadKey(true).Key;

            if (menuNav == ConsoleKey.J)
            {
                Console.WriteLine("Tack för ditt köp! Du loggas nu ut! ");
                Console.WriteLine("Välkommen-ish åter!");
                Thread.Sleep(3000);

                CurrentCustomer?.Cart.Clear();

                foreach (var product in Products)
                {
                    product.Amount = 0;
                }
                MainMenu();
                break;
            }

            if (menuNav == ConsoleKey.N)
            {
                Console.WriteLine("Du skickas tillbaka till huvudmenyn!");
                Thread.Sleep(1500);
                PreShop();
                break;
            }

            Console.WriteLine("Inkorrekt menyval.");
        }
        Console.ReadKey(true);
    }

    private void MenuItem(int menuInput)
    {
        var menuNav = menuInput - 1;

        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Du har valt... {Products[menuNav].Name}!");
            Console.WriteLine();
            Console.WriteLine($"Hur många {Products[menuNav].Name.ToLower()} vill du köpa?");
            int.TryParse(Console.ReadLine(), out var fruitAmount);

            if (fruitAmount == 0)
            {
                Console.WriteLine($"Du har inte lagt till någon {Products[menuNav].Name.ToLower()} i din varukorg.");
                Thread.Sleep(1500);
                break;
            }

            CurrentCustomer?.AddToCart(Products[menuNav], CurrentCustomer.Cart, fruitAmount);

            if (fruitAmount > 100)
            {
                Console.WriteLine();
                Console.WriteLine($"Tycker du verkligen att det där är en rimlig mängd {Products[menuNav].Name.ToLower()}?");
                Thread.Sleep(2000);
                Console.WriteLine();
            }

            if (fruitAmount == 666)
            {
                HighWayToHell();
            }

            Console.WriteLine($"Du har lagt till {fruitAmount} stycken {Products[menuNav].Name.ToLower()} i din varukorg.");
            Thread.Sleep(1500);
            break;
        }
    }

    private void ReadFromFile()
    {
        var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "parheliosLabb2.txt");

        var userName = String.Empty;
        var password = String.Empty;
        var rewardLevel = String.Empty;
        string? line;

        using StreamReader sr = new StreamReader(directory);
        while ((line = sr.ReadLine()) != null)
        {
            if (line.StartsWith("Namn: "))
            {
                userName = line.Substring(5).Trim();
            }
            else if (line.StartsWith("Lösenord: "))
            {
                password = line.Substring(9).Trim();
            }
            else if (line.StartsWith("Varukorg: "))
            {
                continue;
            }
            else if (line.StartsWith("Rabattnivå: "))
            {
                rewardLevel = line.Substring(11).Trim();
            }
            else
            {
                var reward = Enum.Parse<RewardLevel>(rewardLevel);

                var tempUser = new Customer(userName, password, reward);

                if (!CustomerList.Any(c => c.Name == userName))
                {
                    CustomerList.Add(tempUser);
                }
            }
        }
    }

    private void WriteToNewFile()
    {
        var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "parheliosLabb2.txt");

        using StreamWriter sw = new StreamWriter(directory);
        foreach (var customer in CustomerList)
        {
            sw.WriteLine($"Namn: {customer.Name}\n" +
                         $"Lösenord: {customer.GetPassword()}\n" +
                         $"Rabattnivå: {customer.GetRewardLevel()}" +
                         $"\n");
        }
    }
    public void PrintTitle(int part)
    {
        switch (part)
        {
            case 1:
                Console.WriteLine();
                Console.WriteLine("\u2588\u2588\u2593\u2588\u2588\u2588   \u2592\u2588\u2588\u2588\u2588\u2588  \u2584\u2584\u2584\u2588\u2588\u2588\u2588\u2588\u2593 \u2588\u2588\u2593 \u2592\u2588\u2588\u2588\u2588\u2588   \u2588\u2588\u2588\u2584    \u2588      \u2588\u2588\u2588\u2588\u2588\u2588 \u2593\u2588\u2588\u2588\u2588\u2588  \u2588\u2588\u2593     \u2588\u2588\u2593    \u2593\u2588\u2588\u2588\u2588\u2588  \u2588\u2588\u2580\u2588\u2588\u2588  ");
                Thread.Sleep(1000);
                Console.Clear();
                break;
            case 2:
                Console.WriteLine();
                Console.WriteLine("\u2588\u2588\u2593\u2588\u2588\u2588   \u2592\u2588\u2588\u2588\u2588\u2588  \u2584\u2584\u2584\u2588\u2588\u2588\u2588\u2588\u2593 \u2588\u2588\u2593 \u2592\u2588\u2588\u2588\u2588\u2588   \u2588\u2588\u2588\u2584    \u2588      \u2588\u2588\u2588\u2588\u2588\u2588 \u2593\u2588\u2588\u2588\u2588\u2588  \u2588\u2588\u2593     \u2588\u2588\u2593    \u2593\u2588\u2588\u2588\u2588\u2588  \u2588\u2588\u2580\u2588\u2588\u2588  ");
                Console.WriteLine("\u2593\u2588\u2588\u2591  \u2588\u2588\u2592\u2592\u2588\u2588\u2592  \u2588\u2588\u2592\u2593  \u2588\u2588\u2592 \u2593\u2592\u2593\u2588\u2588\u2592\u2592\u2588\u2588\u2592  \u2588\u2588\u2592 \u2588\u2588 \u2580\u2588   \u2588    \u2592\u2588\u2588    \u2592 \u2593\u2588   \u2580 \u2593\u2588\u2588\u2592    \u2593\u2588\u2588\u2592    \u2593\u2588   \u2580 \u2593\u2588\u2588 \u2592 \u2588\u2588\u2592");
                Thread.Sleep(1000);
                Console.Clear();
                break;
            case 3:
                Console.WriteLine();
                Console.WriteLine("\u2588\u2588\u2593\u2588\u2588\u2588   \u2592\u2588\u2588\u2588\u2588\u2588  \u2584\u2584\u2584\u2588\u2588\u2588\u2588\u2588\u2593 \u2588\u2588\u2593 \u2592\u2588\u2588\u2588\u2588\u2588   \u2588\u2588\u2588\u2584    \u2588      \u2588\u2588\u2588\u2588\u2588\u2588 \u2593\u2588\u2588\u2588\u2588\u2588  \u2588\u2588\u2593     \u2588\u2588\u2593    \u2593\u2588\u2588\u2588\u2588\u2588  \u2588\u2588\u2580\u2588\u2588\u2588  ");
                Console.WriteLine("\u2593\u2588\u2588\u2591  \u2588\u2588\u2592\u2592\u2588\u2588\u2592  \u2588\u2588\u2592\u2593  \u2588\u2588\u2592 \u2593\u2592\u2593\u2588\u2588\u2592\u2592\u2588\u2588\u2592  \u2588\u2588\u2592 \u2588\u2588 \u2580\u2588   \u2588    \u2592\u2588\u2588    \u2592 \u2593\u2588   \u2580 \u2593\u2588\u2588\u2592    \u2593\u2588\u2588\u2592    \u2593\u2588   \u2580 \u2593\u2588\u2588 \u2592 \u2588\u2588\u2592");
                Console.WriteLine("\u2593\u2588\u2588\u2591 \u2588\u2588\u2593\u2592\u2592\u2588\u2588\u2591  \u2588\u2588\u2592\u2592 \u2593\u2588\u2588\u2591 \u2592\u2591\u2592\u2588\u2588\u2592\u2592\u2588\u2588\u2591  \u2588\u2588\u2592\u2593\u2588\u2588  \u2580\u2588 \u2588\u2588\u2592   \u2591 \u2593\u2588\u2588\u2584   \u2592\u2588\u2588\u2588   \u2592\u2588\u2588\u2591    \u2592\u2588\u2588\u2591    \u2592\u2588\u2588\u2588   \u2593\u2588\u2588 \u2591\u2584\u2588 \u2592");
                Thread.Sleep(1000);
                Console.Clear();
                break;
            case 4:
                Console.WriteLine();
                Console.WriteLine("\u2588\u2588\u2593\u2588\u2588\u2588   \u2592\u2588\u2588\u2588\u2588\u2588  \u2584\u2584\u2584\u2588\u2588\u2588\u2588\u2588\u2593 \u2588\u2588\u2593 \u2592\u2588\u2588\u2588\u2588\u2588   \u2588\u2588\u2588\u2584    \u2588      \u2588\u2588\u2588\u2588\u2588\u2588 \u2593\u2588\u2588\u2588\u2588\u2588  \u2588\u2588\u2593     \u2588\u2588\u2593    \u2593\u2588\u2588\u2588\u2588\u2588  \u2588\u2588\u2580\u2588\u2588\u2588  ");
                Console.WriteLine("\u2593\u2588\u2588\u2591  \u2588\u2588\u2592\u2592\u2588\u2588\u2592  \u2588\u2588\u2592\u2593  \u2588\u2588\u2592 \u2593\u2592\u2593\u2588\u2588\u2592\u2592\u2588\u2588\u2592  \u2588\u2588\u2592 \u2588\u2588 \u2580\u2588   \u2588    \u2592\u2588\u2588    \u2592 \u2593\u2588   \u2580 \u2593\u2588\u2588\u2592    \u2593\u2588\u2588\u2592    \u2593\u2588   \u2580 \u2593\u2588\u2588 \u2592 \u2588\u2588\u2592");
                Console.WriteLine("\u2593\u2588\u2588\u2591 \u2588\u2588\u2593\u2592\u2592\u2588\u2588\u2591  \u2588\u2588\u2592\u2592 \u2593\u2588\u2588\u2591 \u2592\u2591\u2592\u2588\u2588\u2592\u2592\u2588\u2588\u2591  \u2588\u2588\u2592\u2593\u2588\u2588  \u2580\u2588 \u2588\u2588\u2592   \u2591 \u2593\u2588\u2588\u2584   \u2592\u2588\u2588\u2588   \u2592\u2588\u2588\u2591    \u2592\u2588\u2588\u2591    \u2592\u2588\u2588\u2588   \u2593\u2588\u2588 \u2591\u2584\u2588 \u2592");
                Console.WriteLine("\u2592\u2588\u2588\u2584\u2588\u2593\u2592 \u2592\u2592\u2588\u2588   \u2588\u2588\u2591\u2591 \u2593\u2588\u2588\u2593 \u2591 \u2591\u2588\u2588\u2591\u2592\u2588\u2588   \u2588\u2588\u2591\u2593\u2588\u2588\u2592  \u2590\u258c\u2588\u2588\u2592     \u2592   \u2588\u2588\u2592\u2592\u2593\u2588  \u2584 \u2592\u2588\u2588\u2591    \u2592\u2588\u2588\u2591    \u2592\u2593\u2588  \u2584 \u2592\u2588\u2588\u2580\u2580\u2588\u2584  ");
                Thread.Sleep(1000);
                Console.Clear();
                break;
            case 5:
                Console.WriteLine();
                Console.WriteLine("\u2588\u2588\u2593\u2588\u2588\u2588   \u2592\u2588\u2588\u2588\u2588\u2588  \u2584\u2584\u2584\u2588\u2588\u2588\u2588\u2588\u2593 \u2588\u2588\u2593 \u2592\u2588\u2588\u2588\u2588\u2588   \u2588\u2588\u2588\u2584    \u2588      \u2588\u2588\u2588\u2588\u2588\u2588 \u2593\u2588\u2588\u2588\u2588\u2588  \u2588\u2588\u2593     \u2588\u2588\u2593    \u2593\u2588\u2588\u2588\u2588\u2588  \u2588\u2588\u2580\u2588\u2588\u2588  ");
                Console.WriteLine("\u2593\u2588\u2588\u2591  \u2588\u2588\u2592\u2592\u2588\u2588\u2592  \u2588\u2588\u2592\u2593  \u2588\u2588\u2592 \u2593\u2592\u2593\u2588\u2588\u2592\u2592\u2588\u2588\u2592  \u2588\u2588\u2592 \u2588\u2588 \u2580\u2588   \u2588    \u2592\u2588\u2588    \u2592 \u2593\u2588   \u2580 \u2593\u2588\u2588\u2592    \u2593\u2588\u2588\u2592    \u2593\u2588   \u2580 \u2593\u2588\u2588 \u2592 \u2588\u2588\u2592");
                Console.WriteLine("\u2593\u2588\u2588\u2591 \u2588\u2588\u2593\u2592\u2592\u2588\u2588\u2591  \u2588\u2588\u2592\u2592 \u2593\u2588\u2588\u2591 \u2592\u2591\u2592\u2588\u2588\u2592\u2592\u2588\u2588\u2591  \u2588\u2588\u2592\u2593\u2588\u2588  \u2580\u2588 \u2588\u2588\u2592   \u2591 \u2593\u2588\u2588\u2584   \u2592\u2588\u2588\u2588   \u2592\u2588\u2588\u2591    \u2592\u2588\u2588\u2591    \u2592\u2588\u2588\u2588   \u2593\u2588\u2588 \u2591\u2584\u2588 \u2592");
                Console.WriteLine("\u2592\u2588\u2588\u2584\u2588\u2593\u2592 \u2592\u2592\u2588\u2588   \u2588\u2588\u2591\u2591 \u2593\u2588\u2588\u2593 \u2591 \u2591\u2588\u2588\u2591\u2592\u2588\u2588   \u2588\u2588\u2591\u2593\u2588\u2588\u2592  \u2590\u258c\u2588\u2588\u2592     \u2592   \u2588\u2588\u2592\u2592\u2593\u2588  \u2584 \u2592\u2588\u2588\u2591    \u2592\u2588\u2588\u2591    \u2592\u2593\u2588  \u2584 \u2592\u2588\u2588\u2580\u2580\u2588\u2584  ");
                Console.WriteLine("\u2592\u2588\u2588\u2592 \u2591  \u2591\u2591 \u2588\u2588\u2588\u2588\u2593\u2592\u2591  \u2592\u2588\u2588\u2592 \u2591 \u2591\u2588\u2588\u2591\u2591 \u2588\u2588\u2588\u2588\u2593\u2592\u2591\u2592\u2588\u2588\u2591   \u2593\u2588\u2588\u2591   \u2592\u2588\u2588\u2588\u2588\u2588\u2588\u2592\u2592\u2591\u2592\u2588\u2588\u2588\u2588\u2592\u2591\u2588\u2588\u2588\u2588\u2588\u2588\u2592\u2591\u2588\u2588\u2588\u2588\u2588\u2588\u2592\u2591\u2592\u2588\u2588\u2588\u2588\u2592\u2591\u2588\u2588\u2593 \u2592\u2588\u2588\u2592");
                Thread.Sleep(1000);
                Console.Clear();
                break;
            case 6:
                Console.WriteLine();
                Console.WriteLine("\u2588\u2588\u2593\u2588\u2588\u2588   \u2592\u2588\u2588\u2588\u2588\u2588  \u2584\u2584\u2584\u2588\u2588\u2588\u2588\u2588\u2593 \u2588\u2588\u2593 \u2592\u2588\u2588\u2588\u2588\u2588   \u2588\u2588\u2588\u2584    \u2588      \u2588\u2588\u2588\u2588\u2588\u2588 \u2593\u2588\u2588\u2588\u2588\u2588  \u2588\u2588\u2593     \u2588\u2588\u2593    \u2593\u2588\u2588\u2588\u2588\u2588  \u2588\u2588\u2580\u2588\u2588\u2588  ");
                Console.WriteLine("\u2593\u2588\u2588\u2591  \u2588\u2588\u2592\u2592\u2588\u2588\u2592  \u2588\u2588\u2592\u2593  \u2588\u2588\u2592 \u2593\u2592\u2593\u2588\u2588\u2592\u2592\u2588\u2588\u2592  \u2588\u2588\u2592 \u2588\u2588 \u2580\u2588   \u2588    \u2592\u2588\u2588    \u2592 \u2593\u2588   \u2580 \u2593\u2588\u2588\u2592    \u2593\u2588\u2588\u2592    \u2593\u2588   \u2580 \u2593\u2588\u2588 \u2592 \u2588\u2588\u2592");
                Console.WriteLine("\u2593\u2588\u2588\u2591 \u2588\u2588\u2593\u2592\u2592\u2588\u2588\u2591  \u2588\u2588\u2592\u2592 \u2593\u2588\u2588\u2591 \u2592\u2591\u2592\u2588\u2588\u2592\u2592\u2588\u2588\u2591  \u2588\u2588\u2592\u2593\u2588\u2588  \u2580\u2588 \u2588\u2588\u2592   \u2591 \u2593\u2588\u2588\u2584   \u2592\u2588\u2588\u2588   \u2592\u2588\u2588\u2591    \u2592\u2588\u2588\u2591    \u2592\u2588\u2588\u2588   \u2593\u2588\u2588 \u2591\u2584\u2588 \u2592");
                Console.WriteLine("\u2592\u2588\u2588\u2584\u2588\u2593\u2592 \u2592\u2592\u2588\u2588   \u2588\u2588\u2591\u2591 \u2593\u2588\u2588\u2593 \u2591 \u2591\u2588\u2588\u2591\u2592\u2588\u2588   \u2588\u2588\u2591\u2593\u2588\u2588\u2592  \u2590\u258c\u2588\u2588\u2592     \u2592   \u2588\u2588\u2592\u2592\u2593\u2588  \u2584 \u2592\u2588\u2588\u2591    \u2592\u2588\u2588\u2591    \u2592\u2593\u2588  \u2584 \u2592\u2588\u2588\u2580\u2580\u2588\u2584  ");
                Console.WriteLine("\u2592\u2588\u2588\u2592 \u2591  \u2591\u2591 \u2588\u2588\u2588\u2588\u2593\u2592\u2591  \u2592\u2588\u2588\u2592 \u2591 \u2591\u2588\u2588\u2591\u2591 \u2588\u2588\u2588\u2588\u2593\u2592\u2591\u2592\u2588\u2588\u2591   \u2593\u2588\u2588\u2591   \u2592\u2588\u2588\u2588\u2588\u2588\u2588\u2592\u2592\u2591\u2592\u2588\u2588\u2588\u2588\u2592\u2591\u2588\u2588\u2588\u2588\u2588\u2588\u2592\u2591\u2588\u2588\u2588\u2588\u2588\u2588\u2592\u2591\u2592\u2588\u2588\u2588\u2588\u2592\u2591\u2588\u2588\u2593 \u2592\u2588\u2588\u2592");
                Console.WriteLine("\u2592\u2593\u2592\u2591 \u2591  \u2591\u2591 \u2592\u2591\u2592\u2591\u2592\u2591   \u2592 \u2591\u2591   \u2591\u2593  \u2591 \u2592\u2591\u2592\u2591\u2592\u2591 \u2591 \u2592\u2591   \u2592 \u2592    \u2592 \u2592\u2593\u2592 \u2592 \u2591\u2591\u2591 \u2592\u2591 \u2591\u2591 \u2592\u2591\u2593  \u2591\u2591 \u2592\u2591\u2593  \u2591\u2591\u2591 \u2592\u2591 \u2591\u2591 \u2592\u2593 \u2591\u2592\u2593\u2591");
                Thread.Sleep(1000);
                Console.Clear();
                break;
            case 7:
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Clear();

                Console.WriteLine();
                Console.WriteLine("\u2588\u2588\u2593\u2588\u2588\u2588   \u2592\u2588\u2588\u2588\u2588\u2588  \u2584\u2584\u2584\u2588\u2588\u2588\u2588\u2588\u2593 \u2588\u2588\u2593 \u2592\u2588\u2588\u2588\u2588\u2588   \u2588\u2588\u2588\u2584    \u2588      \u2588\u2588\u2588\u2588\u2588\u2588 \u2593\u2588\u2588\u2588\u2588\u2588  \u2588\u2588\u2593     \u2588\u2588\u2593    \u2593\u2588\u2588\u2588\u2588\u2588  \u2588\u2588\u2580\u2588\u2588\u2588  ");
                Console.WriteLine("\u2593\u2588\u2588\u2591  \u2588\u2588\u2592\u2592\u2588\u2588\u2592  \u2588\u2588\u2592\u2593  \u2588\u2588\u2592 \u2593\u2592\u2593\u2588\u2588\u2592\u2592\u2588\u2588\u2592  \u2588\u2588\u2592 \u2588\u2588 \u2580\u2588   \u2588    \u2592\u2588\u2588    \u2592 \u2593\u2588   \u2580 \u2593\u2588\u2588\u2592    \u2593\u2588\u2588\u2592    \u2593\u2588   \u2580 \u2593\u2588\u2588 \u2592 \u2588\u2588\u2592");
                Console.WriteLine("\u2593\u2588\u2588\u2591 \u2588\u2588\u2593\u2592\u2592\u2588\u2588\u2591  \u2588\u2588\u2592\u2592 \u2593\u2588\u2588\u2591 \u2592\u2591\u2592\u2588\u2588\u2592\u2592\u2588\u2588\u2591  \u2588\u2588\u2592\u2593\u2588\u2588  \u2580\u2588 \u2588\u2588\u2592   \u2591 \u2593\u2588\u2588\u2584   \u2592\u2588\u2588\u2588   \u2592\u2588\u2588\u2591    \u2592\u2588\u2588\u2591    \u2592\u2588\u2588\u2588   \u2593\u2588\u2588 \u2591\u2584\u2588 \u2592");
                Console.WriteLine("\u2592\u2588\u2588\u2584\u2588\u2593\u2592 \u2592\u2592\u2588\u2588   \u2588\u2588\u2591\u2591 \u2593\u2588\u2588\u2593 \u2591 \u2591\u2588\u2588\u2591\u2592\u2588\u2588   \u2588\u2588\u2591\u2593\u2588\u2588\u2592  \u2590\u258c\u2588\u2588\u2592     \u2592   \u2588\u2588\u2592\u2592\u2593\u2588  \u2584 \u2592\u2588\u2588\u2591    \u2592\u2588\u2588\u2591    \u2592\u2593\u2588  \u2584 \u2592\u2588\u2588\u2580\u2580\u2588\u2584  ");
                Console.WriteLine("\u2592\u2588\u2588\u2592 \u2591  \u2591\u2591 \u2588\u2588\u2588\u2588\u2593\u2592\u2591  \u2592\u2588\u2588\u2592 \u2591 \u2591\u2588\u2588\u2591\u2591 \u2588\u2588\u2588\u2588\u2593\u2592\u2591\u2592\u2588\u2588\u2591   \u2593\u2588\u2588\u2591   \u2592\u2588\u2588\u2588\u2588\u2588\u2588\u2592\u2592\u2591\u2592\u2588\u2588\u2588\u2588\u2592\u2591\u2588\u2588\u2588\u2588\u2588\u2588\u2592\u2591\u2588\u2588\u2588\u2588\u2588\u2588\u2592\u2591\u2592\u2588\u2588\u2588\u2588\u2592\u2591\u2588\u2588\u2593 \u2592\u2588\u2588\u2592");
                Console.WriteLine("\u2592\u2593\u2592\u2591 \u2591  \u2591\u2591 \u2592\u2591\u2592\u2591\u2592\u2591   \u2592 \u2591\u2591   \u2591\u2593  \u2591 \u2592\u2591\u2592\u2591\u2592\u2591 \u2591 \u2592\u2591   \u2592 \u2592    \u2592 \u2592\u2593\u2592 \u2592 \u2591\u2591\u2591 \u2592\u2591 \u2591\u2591 \u2592\u2591\u2593  \u2591\u2591 \u2592\u2591\u2593  \u2591\u2591\u2591 \u2592\u2591 \u2591\u2591 \u2592\u2593 \u2591\u2592\u2593\u2591");
                Thread.Sleep(1000);

                PrintSubtitle();
                Thread.Sleep(1000);
                Console.WriteLine("                             (Som bara säljer frukt)");
                Thread.Sleep(2500);
                break;
            case 8:
                Console.WriteLine("\u2588\u2588\u2593\u2588\u2588\u2588   \u2592\u2588\u2588\u2588\u2588\u2588  \u2584\u2584\u2584\u2588\u2588\u2588\u2588\u2588\u2593 \u2588\u2588\u2593 \u2592\u2588\u2588\u2588\u2588\u2588   \u2588\u2588\u2588\u2584    \u2588      \u2588\u2588\u2588\u2588\u2588\u2588 \u2593\u2588\u2588\u2588\u2588\u2588  \u2588\u2588\u2593     \u2588\u2588\u2593    \u2593\u2588\u2588\u2588\u2588\u2588  \u2588\u2588\u2580\u2588\u2588\u2588  ");
                Console.WriteLine("\u2593\u2588\u2588\u2591  \u2588\u2588\u2592\u2592\u2588\u2588\u2592  \u2588\u2588\u2592\u2593  \u2588\u2588\u2592 \u2593\u2592\u2593\u2588\u2588\u2592\u2592\u2588\u2588\u2592  \u2588\u2588\u2592 \u2588\u2588 \u2580\u2588   \u2588    \u2592\u2588\u2588    \u2592 \u2593\u2588   \u2580 \u2593\u2588\u2588\u2592    \u2593\u2588\u2588\u2592    \u2593\u2588   \u2580 \u2593\u2588\u2588 \u2592 \u2588\u2588\u2592");
                Console.WriteLine("\u2593\u2588\u2588\u2591 \u2588\u2588\u2593\u2592\u2592\u2588\u2588\u2591  \u2588\u2588\u2592\u2592 \u2593\u2588\u2588\u2591 \u2592\u2591\u2592\u2588\u2588\u2592\u2592\u2588\u2588\u2591  \u2588\u2588\u2592\u2593\u2588\u2588  \u2580\u2588 \u2588\u2588\u2592   \u2591 \u2593\u2588\u2588\u2584   \u2592\u2588\u2588\u2588   \u2592\u2588\u2588\u2591    \u2592\u2588\u2588\u2591    \u2592\u2588\u2588\u2588   \u2593\u2588\u2588 \u2591\u2584\u2588 \u2592");
                Console.WriteLine("\u2592\u2588\u2588\u2584\u2588\u2593\u2592 \u2592\u2592\u2588\u2588   \u2588\u2588\u2591\u2591 \u2593\u2588\u2588\u2593 \u2591 \u2591\u2588\u2588\u2591\u2592\u2588\u2588   \u2588\u2588\u2591\u2593\u2588\u2588\u2592  \u2590\u258c\u2588\u2588\u2592     \u2592   \u2588\u2588\u2592\u2592\u2593\u2588  \u2584 \u2592\u2588\u2588\u2591    \u2592\u2588\u2588\u2591    \u2592\u2593\u2588  \u2584 \u2592\u2588\u2588\u2580\u2580\u2588\u2584  ");
                Console.WriteLine("\u2592\u2588\u2588\u2592 \u2591  \u2591\u2591 \u2588\u2588\u2588\u2588\u2593\u2592\u2591  \u2592\u2588\u2588\u2592 \u2591 \u2591\u2588\u2588\u2591\u2591 \u2588\u2588\u2588\u2588\u2593\u2592\u2591\u2592\u2588\u2588\u2591   \u2593\u2588\u2588\u2591   \u2592\u2588\u2588\u2588\u2588\u2588\u2588\u2592\u2592\u2591\u2592\u2588\u2588\u2588\u2588\u2592\u2591\u2588\u2588\u2588\u2588\u2588\u2588\u2592\u2591\u2588\u2588\u2588\u2588\u2588\u2588\u2592\u2591\u2592\u2588\u2588\u2588\u2588\u2592\u2591\u2588\u2588\u2593 \u2592\u2588\u2588\u2592");
                Console.WriteLine("\u2592\u2593\u2592\u2591 \u2591  \u2591\u2591 \u2592\u2591\u2592\u2591\u2592\u2591   \u2592 \u2591\u2591   \u2591\u2593  \u2591 \u2592\u2591\u2592\u2591\u2592\u2591 \u2591 \u2592\u2591   \u2592 \u2592    \u2592 \u2592\u2593\u2592 \u2592 \u2591\u2591\u2591 \u2592\u2591 \u2591\u2591 \u2592\u2591\u2593  \u2591\u2591 \u2592\u2591\u2593  \u2591\u2591\u2591 \u2592\u2591 \u2591\u2591 \u2592\u2593 \u2591\u2592\u2593\u2591");
                Console.WriteLine("\u2591\u2592 \u2591       \u2591 \u2592 \u2592\u2591     \u2591     \u2592 \u2591  \u2591 \u2592 \u2592\u2591 \u2591 \u2591\u2591   \u2591 \u2592\u2591   \u2591 \u2591\u2592  \u2591 \u2591 \u2591 \u2591  \u2591\u2591 \u2591 \u2592  \u2591\u2591 \u2591 \u2592  \u2591 \u2591 \u2591  \u2591  \u2591\u2592 \u2591 \u2592\u2591");
                Console.WriteLine("\u2591\u2591       \u2591 \u2591 \u2591 \u2592    \u2591       \u2592 \u2591\u2591 \u2591 \u2591 \u2592     \u2591   \u2591 \u2591    \u2591  \u2591  \u2591     \u2591     \u2591 \u2591     \u2591 \u2591      \u2591     \u2591\u2591   \u2591 ");
                Console.WriteLine("\u2591 \u2591            \u2591      \u2591 \u2591           \u2591          \u2591     \u2591  \u2591    \u2591  \u2591    \u2591  \u2591   \u2591  \u2591   \u2591     ");
                break;
        }
    }

    public static void PrintSubtitle()
    {
        Console.WriteLine("\t _______ _                   __  __ _      _       _       _                 ");
        Console.WriteLine("\t|__   __| |                 / _|/ _(_)    (_)     | |     | |                ");
        Console.WriteLine("\t   | |  | |__   ___    ___ | |_| |_ _  ___ _  __ _| |  ___| |__   ___  _ __  ");
        Console.WriteLine("\t   | |  | '_ \\ / _ \\  / _ \\|  _|  _| |/ __| |/ _` | | / __| '_ \\ / _ \\| '_ \\ ");
        Console.WriteLine("\t   | |  | | | |  __/ | (_) | | | | | | (__| | (_| | | \\__ \\ | | | (_) | |_) |");
        Console.WriteLine("\t   |_|  |_| |_|\\___|  \\___/|_| |_| |_|\\___|_|\\__,_|_| |___/_| |_|\\___/| .__/ ");
        Console.WriteLine("\t                                                                      | |    ");
        Console.WriteLine("\t                                                                      |_|    ");
        Console.WriteLine();
    }

    private void HighWayToHell()
    {
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Clear();
        Console.WriteLine("Du går i en mörk tunnel.");
        Thread.Sleep(2000);
        Console.WriteLine("Du kan knappt se något för all rök.");
        Thread.Sleep(2000);
        Console.WriteLine("Du hör dånande brasor på avstånd.");
        Thread.Sleep(2000);
        Console.WriteLine("Och luften blir hetare och hetare för var steg du går.");
        Thread.Sleep(2000);
        Console.WriteLine($"{CurrentCustomer.Name}, du skulle inte ha köpt så jävla mycket frukt.");
        Thread.Sleep(2000);
        Console.WriteLine("Detta är din skärseld.");
        Thread.Sleep(2000);
        Console.WriteLine("Detta är slutet.");
        Thread.Sleep(5000);
        Process.Start(new ProcessStartInfo { FileName = "https://www.youtube.com/watch?v=pcsi3hU-rX8", UseShellExecute = true });
        Environment.Exit(0);
    }

}