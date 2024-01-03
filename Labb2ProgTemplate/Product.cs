namespace Labb2ProgTemplate
{
    public record Product(string Name, double Price)
    {
        public string Name { get; set; } = Name;
        public double Price { get; set; } = Price;
        public int Amount { get; set; }

        public void AddAmount()
        {
            Amount++;
        }
        public void RemoveAmount()
        {
            Amount--;
        }
    }
}