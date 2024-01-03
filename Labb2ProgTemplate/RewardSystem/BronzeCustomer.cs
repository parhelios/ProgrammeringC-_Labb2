namespace Labb2ProgTemplate.RewardSystem;

public class BronzeCustomer : Customer
{
    public BronzeCustomer(string name, string? password) : base(name, password, RewardLevel.Bronze)
    {
    }
}