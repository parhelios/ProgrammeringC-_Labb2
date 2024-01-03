namespace Labb2ProgTemplate.RewardSystem;

public class SilverCustomer : Customer
{
    public SilverCustomer(string name, string? password) : base(name, password, RewardLevel.Silver)
    {
    }
}