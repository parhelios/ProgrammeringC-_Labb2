namespace Labb2ProgTemplate.RewardSystem;

public class GoldCustomer : Customer
{
    public GoldCustomer(string name, string? password) : base(name, password, RewardLevel.Gold)
    {
    }
}